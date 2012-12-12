function my_delete(id) {
    if (confirm('are you sure you want to delete this gig?')) {
        if (confirm("i don't believe you!!! you're sure?")) {
            $.ajax({ type: "DELETE", url: "/" + id,
                success: function () {
                    alert("done");
                    window.location = "/list";
                },
                error: function () {
                    alert("oooops something went wrong.... i'm sorry");
                }
            });
        }
    }
}