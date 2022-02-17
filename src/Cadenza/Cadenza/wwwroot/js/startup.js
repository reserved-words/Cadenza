
var showOnHover = function (row, show) {
    var cell = row.find('td .show-on-hover');
    if (!cell)
        return;

    cell.css('display', show ? 'block' : 'none');
};

$(function () {

    $('body').on('mouseenter', 'tr', function () {
        showOnHover($(this), true);
    });

    $('body').on('mouseleave', 'tr', function () {
        showOnHover($(this), false);
    });

});