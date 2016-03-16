var toggleable;
$(document).ready(function () {
    toggleable = $('.toggle-control');
});

function onEditButtonClick() {
    toggleable.toggle();
}

function onSuccessCallback() {
    toggleable.toggle();
}
