(function($) {
    'use strict';

    if (!$) {
        throw new Error('Moudle confirm-submit.js is missing jQuery');
    }

    $(document).ready(function() {
        $('[data-confirm]').click(function(e) {
            if (!confirm($(this).attr('data-confirm'))) {
                e.preventDefault();
            }
        });
    });
})(window.jQuery);