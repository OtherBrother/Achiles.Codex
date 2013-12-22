tinymce.init({
    selector: '#ArticleBody',
    entity_encoding: "raw",
    toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | code | table | preview",
    plugins: ["code", "image", "table", "preview"],
    paste_as_text: true
});

