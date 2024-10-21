const sidebarMenuButton = document.querySelector('[data-inr_sidebar-menu-icon]');
const mobileSearchButton = document.querySelector('[data-inr-mb-search-icon]');
const mobileSearchBar = document.querySelector('[data-inr_mbl_search-bar]');
const sidebarMenu = document.querySelector("[data-inr_sidebar-menu]")

function toggleElement(element) {
    if (element.classList.contains("hidden")) {
        element.classList.remove("hidden");
    } else {
        element.classList.add("hidden");
    }
}

function customToggleElement(element, classname) {
    if (element.classList.contains(classname)) {
        element.classList.remove(classname);
    } else {
        element.classList.add(classname);
    }
}

sidebarMenuButton.addEventListener("click", () => customToggleElement(sidebarMenu, "sidebar-open"));
mobileSearchButton.addEventListener("click", () => toggleElement(mobileSearchBar));

//lesson Details form
const lessonDetailsForm = document.querySelector("[data-file-details-form-container]");

//inr_video list functions
//IIFE
const fileHandler = (function () {
    const videoFilesInput = document.querySelector("[data-file-input]")
    const selectFilesButton = document.querySelector("[data-select-files-btn]")
    const videoFileDropZone = document.querySelector("[data-drag-drop-container]")
    const selectedFilesList = document.querySelector("[data-selected-files-list]")
    const uploadingFilesContainer = document.querySelector("[data-uploading-files-container]")
    let selectedFiles = [];
    function updateDropZoneBackground(addClass, removeClass) {
        videoFileDropZone.classList.replace(removeClass, addClass);
    } 
    function handleFileSelection(files) {
        for (let file of files) {
            const existingIndex = selectedFiles.findIndex(existingFile => existingFile.name === file.name);
            if (existingIndex !== -1) {
                selectedFiles.splice(existingIndex, 1, file);
                updateFileListItem(file, existingIndex)
            } else {
                selectedFiles.push(file);
                addFiletoList(file);
            }
        }
        videoFilesInput.value = "";
        updateUiOnFileAdded();
        console.log(selectedFiles)
          }


    function updateFileListItem(file, index) {
        const listItems = selectedFilesList.querySelectorAll('[data-selected-files-list-item]');
        if (listItems[index]) {
            listItems[index].querySelector("[data-selected-file-name]").textContent = file.name;
        }
    }

    function addFiletoList(file) {
        const li = createFileListItem(file)
        selectedFilesList.appendChild(li)
    }

    function createFileListItem(file) {
        const li = document.createElement("li")
        li.classList.add("selected-files-list-item")
        li.setAttribute("data-selected-files-list-item",true)
        li.innerHTML = `
        <p data-selected-file-name class="selected-file-name">${file.name}</p>
        <button data-slelected-file-delete-button class="slelected-file-delete-button" type="button">
            <span class="material-symbols-outlined text-accent-dark_gray hover:text-accent">
                delete
            </span>
        </button>`

        const button = li.querySelector("[data-slelected-file-delete-button]")
        button.addEventListener("click", () => {
            removeFile(file.name)
        })
        return li;
    }

    function removeFile(fileName) {
        const index = selectedFiles.findIndex(file => file.name === fileName);
        if (index !== -1) {
            selectedFiles.splice(index, 1);
            const listItems = selectedFilesList.querySelectorAll('[data-selected-files-list-item]');
            if (listItems[index]) {
                listItems[index].remove();
            }
        }
        updateUiOnFileRemoved()
    }

    function validateFileType(files) {
        for (let file of files) {
            if (file.type !== "video/mp4") {
                alert("only mp4 files are allowed")
                return false;
            }      
        }
        return true;
    }

    function updateUiOnFileAdded() {
        if (selectedFiles.length>1) {
            videoFileDropZone.classList.add("hidden")
            videoFileDropZone.classList.remove("flex")
            uploadingFilesContainer.classList.remove("hidden")
            uploadingFilesContainer.classList.add("flex")
            uploadingFilesContainer.classList.add("uploading-contianer-visible")
            uploadingFilesContainer.classList.remove("uploading-contianer-visible-width")
        }

        if (selectedFiles.length === 1) {
            videoFileDropZone.classList.add("hidden")
            uploadingFilesContainer.classList.remove("hidden")
            uploadingFilesContainer.classList.add("flex")
            lessonDetailsForm.classList.remove("hidden")
            uploadingFilesContainer.classList.remove("uploading-contianer-visible")
            uploadingFilesContainer.classList.add("uploading-contianer-visible-width")
        }
    }

    function updateUiOnFileRemoved() {
        if (selectedFiles.length === 0) {
            videoFileDropZone.classList.remove("hidden")
            videoFileDropZone.classList.add("flex")
            uploadingFilesContainer.classList.add("hidden")
            uploadingFilesContainer.classList.remove("flex")
            lessonDetailsForm.classList.add("hidden")
        }
    }


    return {
        init: function() {
            videoFileDropZone.addEventListener("dragover", (e) => {
                e.preventDefault();
                updateDropZoneBackground("dragover", "bg-white")
            })
            videoFileDropZone.addEventListener("dragleave", (e) => {
                e.preventDefault();
                updateDropZoneBackground("bg-white", "dragover")
            })
            videoFileDropZone.addEventListener("drop", (e) => {
                e.preventDefault();
                updateDropZoneBackground("bg-white", "dragover")
                const valid = validateFileType(e.dataTransfer.files);

                if (!valid) {
                    return;
                }
                
                videoFilesInput.files = e.dataTransfer.files;
                
                videoFilesInput.dispatchEvent(new Event("change"));
            })
            selectFilesButton.addEventListener("click", () => {
                videoFilesInput.click();
            })

            videoFilesInput.addEventListener("change", (e) => {
                handleFileSelection(e.target.files)
            })
        },
        getSelectedFiles: function () {
            return [...selectedFiles];
        }
    }
})();

fileHandler.init();


//Thumbnail choosing logic
const selectThumbnailIcon = document.querySelector("[data-select-thumbnail-icon]")
const selectThubmnailFileInuput = document.querySelector("[data-select-thumbnail-input]")
const thumbnailPreviewImage = document.querySelector("[data-thumbnail-preview-image]")
const thumbnailSpinner = document.querySelector("[data-thumbnail-preview-spinner]")
const removeThumbnailIcon = document.querySelector("[data-remove-thumbnail-icon]")
function readFile(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = (e) => resolve(e.target.result);
        reader.onerror = (e) => reject(new Error('Error reading file',e));
        reader.readAsDataURL(file);
    });
}

selectThumbnailIcon.addEventListener("click", () => {
    selectThubmnailFileInuput.click();
    
})
removeThumbnailIcon.addEventListener("click", () => {
    selectThubmnailFileInuput.value = "";
    thumbnailPreviewImage.classList.add("hidden")
    thumbnailSpinner.classList.add("hidden")
    selectThumbnailIcon.classList.remove("!hidden")
})

selectThubmnailFileInuput.addEventListener("change", (e) => {

    const thubmnailImage = e.target.files[0];
    //const validImageTypes = ['image/jpeg', 'image/png', 'image/gif'] 

    //if (thumbnailImage && !validImageTypes.includes(thubmnailImage.type)) {
    //    alert("only .jpg, .png or .gif files are allowed")
    //    return;
    //}
    thumbnailSpinner.classList.remove("hidden")

    if (thubmnailImage) {
        readFile(thubmnailImage).then((result) => {
            if (result) {
                thumbnailPreviewImage.src = `${result}`
                thumbnailPreviewImage.classList.remove("hidden")
                thumbnailSpinner.classList.add("hidden")
                selectThumbnailIcon.classList.add("!hidden")
            }
        })
    }
})


//actual videe uploading logic
//IIFE

const uploadHandling = (function () {
    const uploadVideoButton = document.querySelector("[data-files-upload-button]")
    const filesUploadingContainer = document.querySelector("[data-files-uploading-container]")
    const filesUploadingList = document.querySelector("[data-files-uploading-list]")
    const filesToUploadList = document.querySelector("[data-files-upload-part]")

    function updateUIonUploadStart() {
        filesUploadingList.classList.replace("hidden", "flex")
        filesToUploadList.classList.replace("flex", "hidden")
    }

     function createFileUploadListItem(file) {
        const li = document.createElement("li")
        li.classList.add("files-uploading-list-item")
        li.setAttribute("data-files-uploading-list-item", true)
         li.innerHTML = `
        <div data-file-uploading-list-section class="file-uploading-list-section">
        <p data-file-uploading-name class="file-uploading-name">${file.name}</p>
        <button data-file-uploading-cancel-button class="file-uploading-cancel-button" type="button">
            <span class="material-symbols-outlined text-accent-dark_gray hover:text-accent">
                close
            </span>
        </button>
        <progress
        data-file-uploading-progress
        class="file-uploading-progress"
        low="10"
        high="90"
        max="100"
        value="0">
        0%
        </progress>
        </div>`

        const button = li.querySelector("[data-file-uploading-cancel-button]")
        button.addEventListener("click", () => {
            removeFile(file.name)
        })
        return li;
    }

    function addFileUploadingtoList(file) {
        const li = createFileUploadListItem(file)
        filesUploadingList.appendChild(li)
    }
  
    function uploadFile(file) {
        const req = new XMLHttpRequest();
        req.open("POST", "/Instructor/UploadLessonVideo", true)

        req.onload = function () {
            if (this.status === 200) {
                console.log(this.response)
            }
        }

        req.onerror = function () {
            console.log(this.response)
        }
    }

      function uploadFiles(files) {
        for (let file of files) {
             uploadFile(file)
        }

    }

    const filesToUpload = fileHandler.getSelectedFiles();

    if (uploadVideoButton) {
        uploadVideoButton.addEventListener("click", () => {
            if (filesToUpload.length > 0) {
                alert("do not leave or refresh the page while video uploads go on")
                uploadFiles(file)
            }
            
        })

    }

})();









