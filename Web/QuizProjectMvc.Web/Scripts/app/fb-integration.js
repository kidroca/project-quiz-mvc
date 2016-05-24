(function ($, window) {
    window.fbAsyncInit = function () {
        FB.init({
            appId: '461214147401172',
            xfbml: true,
            version: 'v2.5'
        });
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

    $(document).ready(function() {
        $('.fb-share').on('click', function() {
            var $this = $(this);
            console.log($this);

            var shareLink = window.location.origin + $this.attr('data-share-url');
            console.log(shareLink);

            FB.ui({
                method: 'share',
                href: shareLink
            }, function(response) {
                console.log('fb response --> ', response);
            });
        });
    });
})(window.jQuery, window);