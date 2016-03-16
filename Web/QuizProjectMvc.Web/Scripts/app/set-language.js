$(document).ready(function () {
    $('.language').on('click', 'a[data-lang]', function (event) {
        var $this = $(this);
        var lang = $this.attr('data-lang');
        if (lang) {
            document.cookie = 'language=' + lang;
            window.location.reload();
        }
    });
});