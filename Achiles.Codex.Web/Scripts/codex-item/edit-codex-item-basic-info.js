
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
var tags = $('#CodexItem_Tags').tagsinput('input');
tags.typeahead([
    {
        limit: 10,
        name: 'tags',
        remote: window.codex.urls.tagsSuggestUrl + '%QUERY',
    }
]);

Handlebars.registerHelper('objectTypeName', function (objtype) {
    return window.codex.objectTypeNames[objtype];
});

$('#CodexItem_RelatedItems')
    .tagsinput({
        confirmKeys: [13, 44, 9]
    });
var relatedItemsInput = $('#CodexItem_RelatedItems').tagsinput('input');

relatedItemsInput.typeahead([
    {
        limit: 10,
        name: 'items',
        valueKey: 'Id',
        remote: window.codex.urls.codexItemSuggestUrl + '/%QUERY',
        template: '<h5><strong>{{Name}}</strong> <small class="pull-right">{{{objectTypeName ObjectType}}}</small></h5> ',
        engine: T,
    }
]);