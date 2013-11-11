   
function EditTag(id) {
    var s = '#' + id;
    s = $(s).val();
        $.ajax({
            url: '/Tag/EditTag',             
            type: "POST",                     
            username: "User.Identity.Name",
            data: "Id=" + id + "&Name=" + s,
            datatype: "html",
            success: function(html) {
                if (html != "Good") {
                    alert("Sorry, changing not save.");
                }
            }
        });
}

function DeleteTag(id) {
    var r = confirm("Data will delete! Are you sure ?");
    if (r == true) {
        var path = "#tagsTable >." + id;
        var objRemove = $(path);
        $.ajax({
            url: '/Tag/DeleteTag',
            type: "Post",
            username: "User.Identity.Name",
            data: "Id=" + id,
            datatype: "html",
            success: function (html) {
                if (html != "Good")
                    alert("You can't delete this tag.");
                else {
                    objRemove.remove();
                }
            }
        });
    } else {
        return;
    }
}
