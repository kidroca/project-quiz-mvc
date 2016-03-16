(function() {
    'use strict';

    $(document).ready(function() {
        $('[data-confirm]').click(function(e) {
            if (!confirm($(this).attr('data-confirm'))) {
                e.preventDefault();
            }
        });
    });
})();