window.ShowToastr = function(type, message) {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 1000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 1000 });
    }
}