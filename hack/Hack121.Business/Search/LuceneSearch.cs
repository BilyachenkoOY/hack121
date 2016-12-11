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

        private static void _addToLuceneIndex(TransactionSearchData TransactionSearchData, IndexWriter writer)
        {
            // remove older index entry
            var searchQuery = new TermQuery(new Term("Id", TransactionSearchData.Id));
            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var doc = new Document();

            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", TransactionSearchData.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
//            doc.Add(new Field("PayerEdrpo", TransactionSearchData.PayerEdrpo, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("PaymentDetails", TransactionSearchData.PaymentDetails, Field.Store.YES, Field.Index.ANALYZED));

            // add entry to index
            writer.AddDocument(doc);
        }

        public static void AddUpdateLuceneIndex(IEnumerable<TransactionSearchData> transactionDatas)
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

        public static void AddUpdateLuceneIndex(TransactionSearchData transaction)
        {
            AddUpdateLuceneIndex(new List<TransactionSearchData> { transaction });
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

        private static TransactionSearchData _mapLuceneDocumentToData(Document doc)
        {
            return new TransactionSearchData
            {
                Id = doc.Get("Id"),
                PaymentDetails = doc.Get("PaymentDetails")
            };
        }

        private static IEnumerable<TransactionSearchData> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }
        private static IEnumerable<TransactionSearchData> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
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

        private static IEnumerable<TransactionSearchData> _search (string searchQuery, string searchField = "")
        {
            // validation
            if (searchQuery.Replace("*", "").Replace("?", "").IsEmpty()) return new List<TransactionSearchData>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                var hits_limit = 1000;
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

        public static IEnumerable<TransactionSearchData> Search(string input, string fieldName = "")
        {
            if (input.IsEmpty())
                return new List<TransactionSearchData>();

            var terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !x.IsEmpty()).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);

            return _search(input, fieldName);
        }

        public static IEnumerable<TransactionSearchData> SearchDefault(string input, string fieldName = "")
        {
            return input.IsEmpty() ? new List<TransactionSearchData>() : _search(input, fieldName);
        }
    }
}
