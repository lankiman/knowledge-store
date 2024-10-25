 const toastHandler = (function () {
     const toastTemplate = document.querySelector("[data-toast-template]")

     console.log(toastTemplate)
    
    const templateContentNode = toastTemplate.content.cloneNode(true);

    const toastMessage = templateContentNode.querySelector("[data-toast-message]")

    const toastMessageIcon = templateContentNode.querySelector("[data-toast-icon]")

    const toastMessageLine = templateContentNode.querySelector("[data-toast-line]")



     function showToast(message, type, element) {

         console.log(element)
         console.log("i have been called")
         console.log(templateContentNode)
        
        element.appendChild(templateContentNode)
        toastMessage.textContent = message
        if (type == "error") {
            toastMessageIcon.textContent="error"
        }
        if (type == "success") {
            toastMessageIcon.textContent="task_alt"
        }
    }



    return {
        showToast:showToast
    }

})()

export default toastHandler;



