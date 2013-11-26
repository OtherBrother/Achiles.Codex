
var T = {};
T.compile = function (template) {
    var compile = Handlebars.compile(template),
        render = {
            render: function (ctx) {
                return compile(ctx);
            }
        };
    return render;
};

$('#CodexItem_Tags').tagsinput();

Handlebars.registerHelper('objectTypeName', function (objtype) {
    return window.codex.objectTypeNames[objtype];
});
$('#CodexItem_RelatedItems')
    .tagsinput({
        confirmKeys: [13, 44, 9]
    });
var input = $('#CodexItem_RelatedItems').tagsinput('input');

input.typeahead([
    {
        limit: 10,
        name: 'items',
        valueKey: 'Id',
        remote: window.codex.urls.codexItemSuggestUrl + '/%QUERY',
        template: '<h5><strong>{{Name}}</strong> <small class="pull-right">{{{objectTypeName ObjectType}}}</small></h5> ',
        engine: T,
    }
]);