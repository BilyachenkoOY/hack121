using Hack121.Business.Entities;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hack121.Business.Search
{
    public static class LuceneSearch
    {
        private static string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "lucene_index");
        private static FSDirectory _directoryTemp;

        const Lucene.Net.Util.Version version = Lucene.Net.Util.Version.LUCENE_30;

        private static FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(_directoryTemp))
                    IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath))
                    File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

        private static void _addToLuceneIndex(PaymentTransaction trans, IndexWriter writer)
        {
            // remove older index entry
            var searchQuery = new TermQuery(new Term("Id", trans.Id));
            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var doc = new Document();

            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", trans.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("PayerEdrpo", trans.PayerEdrpo, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("PaymentDetails", trans.PaymentDetails.Replace(".", ". "), Field.Store.YES, Field.Index.ANALYZED));

            doc.Add(new Field("CategoryId", trans.CategoryId ?? string.Empty, Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Price", trans.Price.ToString(), Field.Store.YES, Field.Index.NO));

            // add entry to index
            writer.AddDocument(doc);
        }

        public static void AddUpdateLuceneIndex(IEnumerable<PaymentTransaction> transactionDatas)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (var data in transactionDatas) _addToLuceneIndex(data, writer);

                // close handles
                analyzer.Close();
            }
        }

        public static void AddUpdateLuceneIndex(PaymentTransaction transaction)
        {
            AddUpdateLuceneIndex(new List<PaymentTransaction> { transaction });
        }

        public static void ClearLuceneIndexRecord(int record_id)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index entry
                var searchQuery = new TermQuery(new Term("Id", record_id.ToString()));
                writer.DeleteDocuments(searchQuery);

                // close handles
                analyzer.Close();
            }
        }

        public static bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(version);
                using (var writer = new IndexWriter(_directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();

                    // close handles
                    analyzer.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void Optimize()
        {
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
            }
        }

        private static PaymentTransaction _mapLuceneDocumentToData(Document doc)
        {
            return new PaymentTransaction
            {
                Id = doc.Get("Id"),
                PaymentDetails = doc.Get("PaymentDetails"),
                PayerEdrpo = doc.Get("PayerEdrpo"),
                CategoryId = doc.Get("CategoryId"),
                Price = decimal.Parse(doc.Get("Price"))
            };
        }

        private static IList<PaymentTransaction> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }
        private static IList<PaymentTransaction> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }

        private static Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private static IList<PaymentTransaction> _search(string searchQuery, string searchField = "")
        {
            // validation
            if (searchQuery.Replace("*", "").Replace("?", "").IsEmpty())
                return new List<PaymentTransaction>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                var hits_limit = short.MaxValue;
                var analyzer = new StandardAnalyzer(version);

                // search by single field
                if (!searchField.IsEmpty())
                {
                    var parser = new QueryParser(version, searchField, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher
                        .Search(query, hits_limit)
                        .ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    return results;
                }
                // search by multiple fields (ordered by RELEVANCE)
                else
                {
                    var parser = new MultiFieldQueryParser(version, new[] { "Id", "PaymentDetails" }, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher
                        .Search(query, null, hits_limit, Sort.RELEVANCE)
                        .ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    return results;
                }
            }
        }

        public static IList<PaymentTransaction> Search(string input, string fieldName = "")
        {
            if (input.IsEmpty())
                return new List<PaymentTransaction>();

            var terms = input.Trim()
                .Replace("-", " ")
                .Split(' ')
                .Where(x => !x.IsEmpty()).Select(x => x.Trim());
            input = string.Join(" ", terms);

            return _search(input, fieldName);
        }

        public static IEnumerable<PaymentTransaction> SearchByCategory(string input)
        {
            return Search(input, "PaymentDetails").Where(pt => pt.CategoryId.IsEmpty());
        }

        public static IEnumerable<PaymentTransaction> SearchDefault(string input, string fieldName = "")
        {
            return input.IsEmpty() ? Enumerable.Empty<PaymentTransaction>() : _search(input, fieldName);
        }
    }
}
