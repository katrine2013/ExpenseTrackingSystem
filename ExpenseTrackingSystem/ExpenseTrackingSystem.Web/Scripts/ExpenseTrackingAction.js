function DeleteExpense(id) {
    var r = confirm("Data will delete! Are you sure ?");
    if (r == true) {
        var path = "#expensesTable >." + id;
        var objRemote = $(path);
        $.ajax({
            url: '/Expense/DeleteExpense',
            type: "Post",
            username: "User.Identity.Name",
            data: "Id=" + id,
            datatype: "html",
            success: function (html) {
                if (html != "Good")
                    alert("Record wasn't delete.");
                else {
                    objRemote.remove();
                }
            }
        });
    } else {
        return;
    }
}
