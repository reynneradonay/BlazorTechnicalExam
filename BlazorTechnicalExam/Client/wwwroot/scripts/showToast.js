window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Success");
    }
    if (type === "error") {
        toastr.error(message, "Error");
    }

    if (type === "info") {
        toastr.info(message, "Info");
    }

    if (type === "warning") {
        toastr.warning(message, "Warning");
    }
}

window.ClearToastr = () => {
    toastr.clear();
}