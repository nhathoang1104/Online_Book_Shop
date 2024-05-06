var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData1').DataTable({
        "ajax": { url:'/admin/order/getall' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'tenNguoiNhan', "width": "20%" },
            { data: 'address', "width": "25%" },
            { data: 'sdt', "width": "10%" },
            { data: 'createDate', "width": "10%" },
            { data: 'orderStatusId', "width": "5%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/Admin/Order/Deliver?orderId=${data}" class="btn btn-primary"><i class="bi bi-box-seam-fill"></i></a>
                     <a href="/Admin/Order/Details?orderId=${data}" class="btn btn-info"><i class="bi bi-arrow-right-square-fill"></i></a>
                     <a href="/Admin/Order/CompleteOrder?orderId=${data}" class="btn btn-success"><i class="bi bi-check-lg"></i></a>
                    </div>`
                },
                "width": "30%"
            }
        ]
    });
}