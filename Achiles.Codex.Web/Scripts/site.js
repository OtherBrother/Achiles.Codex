var binder = binder || {};
binder.bind = function (container, template, addObj, data, onAddFunc, ctx) {
    var c = $(container);
    var s = $(template).html();
    var t = Handlebars.compile(s);
    var index = 0;

    var create = function (val) {
        var html = t({ index: index, value: val, context: ctx });
        c.append(html);

        if (typeof (onAddFunc) == "function") {
            onAddFunc(index, val, ctx);
        }
        index++;
    };

    $(addObj).click(function () { create(); });

    _.each(data, function (x) { create(x); });
};

Handlebars.registerHelper('create-option', function (value, actual) {
    var option = [];
    option.push('<option value="', value, '"', ' ');

    if (value == actual)
        option.push('selected');

    option.push('>', value, '</option>');

    return new Handlebars.SafeString(option.join(''));
});
