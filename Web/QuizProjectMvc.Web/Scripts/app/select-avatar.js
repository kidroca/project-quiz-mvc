(function ($) {
    'use strict';

    if (!$) {
        throw new Error('Moudle select-avatar.js is missing jQuery');
    }

    $(document).ready(function () {
        var selectedAvatarPlaceholder = $('#selected-avatar');
        var selectedAvatarFormField = $('#AvatarUrl');
        var prevSelected = $('<div/>');

        $('div[data-id="avatars"').on('click', 'img', function(ev) {
            var $this = $(this);

            prevSelected.removeClass('selected');
            prevSelected = $this;
            $this.addClass('selected');
            
            var src = $this.attr('src');
            if (src) {
                selectedAvatarPlaceholder.attr('src', src);
                selectedAvatarFormField.val(src);
            }
        });
    });
})(window.jQuery);