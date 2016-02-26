(function () {
    $(document).ready(function () {
        var selectedAvatarPlaceholder = $('#selected-avatar');
        var selectedAvatarFormField = $('#AvatarUrl');

        $('div[data-id="avatars"').on('click', 'img', function(ev) {
            var $this = $(this);
            var src = $this.attr('src');
            if (src) {
                selectedAvatarPlaceholder.attr('src', src);
                selectedAvatarFormField.val(src);
            }
        });
    });
})();