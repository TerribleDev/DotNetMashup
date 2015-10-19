// Write your Javascript code.
$(document).ready(function(){
    var page = @Model.CurrentPage;
    $('img').addClass('img-responsive');
    $('.tile').addClass('show');
    var loading = false;
    $(window).on('scroll', function () {
        var loadingImage = $('.loadWheel');
        if($(window).scrollTop() + $(window).height() == $(document).height() && !loading )  {
            loading = true
            loadingImage.show();
            $.ajax('/Home/Tiles', {data:{page:page+1}})
            .done(function(data){
                page++;
                loading = false;
                loadingImage.hide();
                var resd = $(data);
                resd.find('img').addClass('img-responsive');
                $('#tilesContainer').append(resd).find('.tile').addClass('show');
            })
            .error(function(){
                loadingImage.hide();
            });
        }
    });
});
(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-48128396-3', 'auto');
ga('send', 'pageview');