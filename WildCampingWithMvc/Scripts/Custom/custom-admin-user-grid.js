$(function () {
    $("#grid").jqGrid({
        url: "/Admin/User/GetUsers",
        datatype: "json",
        mtype: "Get",
        colNames: ["ID", "First Name", "Last Name", "User Name", "Registered On"],
        colModel: [
            { name: "Id", key: true, index: "Id", hidden: true },
            { name: "FirstName", index: "FirstName", align: "center", editable: true, sortable: true },
            { name: "LastName", index: "LastName", align: "center", editable: true, sortable: true },
            { name: "UserName", index: "UserName", align: "left", sortable: true },
            {
                name: "RegisteredOn", index: "RegisteredOn", formatter: "date",
                formatoptions: { srcformat: "d/m/Y", newformat: "d/m/Y" },
                align: "right", sortable: true
            }
        ],

        height: "auto",
        autowidth: "true",
        rowNum: 10,
        loadonce: true,
        rowList: [10, 20],
        pager: "pager",
        sortName: "FirstName",
        viewrecords: true,
        sortorder: "asc",
        caption: "List of users",
        emptyrecords: "No users found",
        jsonreader: {
            repeatitems: false,
            Id: "0"
        },
        multiselect: false
    }).navGrid("#pager", { edit: true, add: false, del: true, search: true, refresh: true },
    {
        // EDIT OPTIONS
        width: 600,
        url: "/Admin/User/UpdateUser",
        closeAfterEdit: true,
        afterComplete: function (response) {
            $("#grid").setGridParam({ datatype: "json", page: 1 }).trigger("reloadGrid");
            alert(response.responseText);
        }
    },
    {
        // ADD OPTIONS
    },
    {
        // DELETE OPTIONS
        width: 600,
        url: "/Admin/User/DeleteUser",
        closeAfterDelete: true,
        msg: "Are you really willing to delete these data?",
        afterComplete: function (response) {
            $("#grid").setGridParam({ datatype: "json", page: 1 }).trigger("reloadGrid");
            alert(response.responseText);
        }
    },
    {
        // SEARCH OPTIONS
        width: 600,
        multipleSearch: true
    }
    );
})