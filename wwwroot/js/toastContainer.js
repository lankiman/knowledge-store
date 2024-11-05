 const toastHandler = (function () {
     const toastTemplate = document.querySelector("[data-toast-template]")
     const toastPlaceholder = document.querySelector("[data-toast-placeholder]")

     function updateState(element, removeStyle, addStyle) {
         element.classList.remove(removeStyle)
         element.classList.add(addStyle)
     }

 

     function showToast(message, type) {
         const toastTemplateContent = toastTemplate.content.cloneNode(true)

         console.log(toastTemplateContent)


         const toastMessage = toastTemplateContent.querySelector("[data-toast-message]")

         const toastMessageIcon = toastTemplateContent.querySelector("[data-toast-icon]")

         const toastMessageLine = toastTemplateContent.querySelector("[data-toast-line]")
        
         toastPlaceholder.appendChild(toastTemplateContent)

         if (toastPlaceholder.children.length > 0) {
             toastPlaceholder.classList.replace("hidden", "flex")
         }

        toastMessage.textContent = message
        if (type == "error") {
            toastMessageIcon.textContent = "error"
            updateState(toastMessageIcon, "toast-icon-success", "toast-icon-error")
            updateState(toastMessageLine, "toast-line-success", "toast-line-error")
            updateState(toastMessage, "toast-message-success", "toast-message-error")

        }
        if (type == "success") {
            toastMessageIcon.textContent = "task_alt"
            updateState(toastMessageIcon, "toast-icon-error", "toast-icon-success")
            updateState(toastMessageLine, "toast-line-error", "toast-line-success")
            updateState(toastMessage, "toast-message-error", "toast-message-success")
         }
         const appendedToast = toastPlaceholder.lastElementChild;
         setTimeout(() => {
             updateState(appendedToast, "toastIn", "toastOut")
             appendedToast.addEventListener("animationend", () => {
                 toastPlaceholder.removeChild(appendedToast);
                 if (toastPlaceholder.children.length <1) {
                     toastPlaceholder.classList.add("hidden");
                 }
             });
         }, 4200)

         
    }

    return {
        showToast:showToast
    }

})()

export default toastHandler;



