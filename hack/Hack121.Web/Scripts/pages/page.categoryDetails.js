var categoryDetailsPage = (function(){
    var init = function(){
        bindUIEvents();
    };

    var bindUIEvents = function(){
        $(".positive-vote").on('click', function(){
            var counter = $(".positive-count");
            var value = counter.attr("data-value");
            counter.attr("data-value", ++value);
            counter.html(value);
            // $.post();#warning add social sharing + counter
        });

        $(".negative-vote").on('click', function(){
            var counter = $(".negative-count");
            var value = counter.attr("data-value");
            counter.attr("data-value", --value);
            counter.html(value);
            // $.post(); #warning add social sharing + counter
        });
    }

    return {
        Init: init
    };
})();