
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

$('#CodexItem_Tags')
.tagsinput({
    confirmKeys: [13, 44]
});
var tags = $('#CodexItem_Tags').tagsinput('input');
tags.typeahead([
    {
        limit: 10,
        name: 'tags',
        remote: window.codex.urls.tagsSuggestUrl + '%QUERY',
    }
]).bind('typeahead:selected', $.proxy(function (obj, datum) {
    
    this.tagsinput('add', datum.value);
    this.tagsinput('input').typeahead('setQuery', '');
    
}, $('#CodexItem_Tags')));

Handlebars.registerHelper('objectTypeName', function (objtype) {
    return window.codex.objectTypeNames[objtype];
});

$('#CodexItem_RelatedItems')
    .tagsinput({
        confirmKeys: [13, 44]
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
]).bind('typeahead:selected', $.proxy(function (obj, datum) {

    console.log(datum);
    this.tagsinput('add', datum.Id);
    this.tagsinput('input').typeahead('setQuery', '');

}, $('#CodexItem_RelatedItems')));