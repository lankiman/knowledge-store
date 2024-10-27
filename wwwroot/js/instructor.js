import toastHandler from './toastContainer.js';

const sidebarMenuButton = document.querySelector('[data-inr_sidebar-menu-icon]');
const mobileSearchButton = document.querySelector('[data-inr-mb-search-icon]');
const mobileSearchBar = document.querySelector('[data-inr_mbl_search-bar]');
const sidebarMenu = document.querySelector("[data-inr_sidebar-menu]")




//General Helper Functions
const sizeConverter = (size) => {
    let units = ["bytes", "kb", "mb", "gb"];
    let unitIndex = 0;

    while (size >= 1024 && unitIndex < units.length - 1) {
        size /= 1024;
        unitIndex++;
    }
    return `${size.toFixed(2)} ${units[unitIndex]}`;
};




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
        
        if (selectedFiles.length === 1) {
            lessonDetailsForm.classList.remove("hidden")
            uploadingFilesContainer.classList.remove("uploading-contianer-visible")
            uploadingFilesContainer.classList.add("uploading-contianer-visible-width")
        }
    }


    return {
        init: function () {
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
        },
        clearUi: function () {
            videoFileDropZone.classList.remove("hidden")
            videoFileDropZone.classList.add("flex")
            uploadingFilesContainer.classList.add("hidden")
            uploadingFilesContainer.classList.remove("flex")
            lessonDetailsForm.classList.add("hidden")
            selectedFilesList.innerHTML=""
        },
        clearSelectedFiles: function () {
            selectedFiles=[]
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


function clearThumbnial() {
    selectThubmnailFileInuput.value = "";
    thumbnailPreviewImage.classList.add("hidden")
    thumbnailSpinner.classList.add("hidden")
    selectThumbnailIcon.classList.remove("!hidden")
    selectThumbnailIcon.style.display = "block"

    selectThubmnailFileInuput.dispatchEvent(new Event("change"))
}
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
  clearThumbnial()

})



selectThubmnailFileInuput.addEventListener("change", (e) => {

    const thubmnailImage = e.target.files[0];
    //const validImageTypes = ['image/jpeg', 'image/png', 'image/gif']

    //if (thumbnailImage && !validImageTypes.includes(thubmnailImage.type)) {
    //    alert("only .jpg, .png or .gif files are allowed")
    //    return;
    //}
   
    if (thubmnailImage) {
        thumbnailSpinner.classList.remove("hidden")
        readFile(thubmnailImage).then((result) => {
            if (result) {
                thumbnailPreviewImage.src = `${result}`
                thumbnailPreviewImage.classList.remove("hidden")
                thumbnailSpinner.classList.add("hidden")
                selectThumbnailIcon.classList.add("!hidden")
                selectThumbnailIcon.style.display="none"
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
    const completeVideoDetailsForm = document.querySelector("[data-video-details-form]")
    const completeVideoDetailsFormButton = document.querySelector("[data-complete-video-details-button]")

    let uploadedLessonId = "";

    let uploadQueue = []
    let isUploading = false;
    let currentReq = null;
    let currentFile = null;
    let uploadedFiles=[]


    function updateUiOnComplete() {
        filesToUploadList.classList.replace("flex", "hidden")
        filesUploadingContainer.classList.replace("hidden", "flex")
    }

    function updateUiOnCancel() {
        filesToUploadList.classList.replace("hidden", "flex")
        filesUploadingContainer.classList.replace("flex", "hidden")

    }


    function updateVideoDetailsFormStatus(lessonId) {
        completeVideoDetailsForm.action = "/Instructor/CompleteLessonDetails?Id=" + encodeURIComponent(lessonId);
        completeVideoDetailsFormButton.removeAttribute("disabled")
        completeVideoDetailsFormButton.classList.remove("disabled")
    }
    function updateUIonUploadStart() {
        filesToUploadList.classList.replace("flex", "hidden")
        filesUploadingContainer.classList.replace("hidden", "flex")
    }


    function updateUploadProgress(progress, file) {
        const progressBar = filesUploadingList.querySelector(`[data-file-uploading-list-section="${file.name}"]`)
            ?.querySelector('[data-file-uploading-progress]');
        if (progressBar) {
            progressBar.value = progress;
        }
        const progressBarText = filesUploadingList.querySelector(`[data-file-uploading-list-section="${file.name}"]`)
            ?.querySelector('[data-file-upload-progress-text]');
        if (progressBarText) {
            progressBarText.textContent=`${Math.trunc(progress)}% of ${sizeConverter(file.size)}`
        }
    }

    function replaceCancelButtonWithCheck(file) {
        const cancelButton = filesUploadingList.querySelector(`[data-file-uploading-list-section="${file.name}"]`)
            ?.querySelector('[data-file-uploading-cancel-button]');
        if (cancelButton) {
            cancelButton.innerHTML =`
            <span class="material-symbols-outlined uploaded-check-icon">
                check
            </span>
            `
            cancelButton.setAttribute("disabled", true)
        }
    }

    function abortUploadReq(fileName) {
        if (currentFile.name === fileName && currentReq) {
            currentReq.abort()
        }
    }

    function cancelFileUPload(fileName, li) {
        const index = uploadQueue.findIndex(file => file.name === fileName);
        if (index !== -1) {
            uploadQueue.splice(index, 1);
            li.remove();
            console.log(uploadedFiles.length, uploadQueue.length)
            if (uploadQueue.length === 0 && uploadFiles.length === 0) {
                updateUiOnCancel();
            }
        }
        abortUploadReq(fileName);
        
        
    }


    

     function createFileUploadListItem(file) {
        const li = document.createElement("li")
        li.classList.add("files-uploading-list-item")
        li.setAttribute("data-files-uploading-list-item", true)
        /*li.setAttribute(`data-files-uploading-list-item-for=${file.name}`, true)*/
        li.innerHTML = `
        <p data-file-uploading-name class="file-uploading-name">${file.name}</p>
        <div data-file-uploading-list-section="${file.name}" class="file-uploading-list-section">
            <div data-file-upload-progress-container class=file-upload-progress-container>
                <progress
                data-file-uploading-progress
                class="file-uploading-progress"
                low="10"
                high="90"
                max="100"
                value="0">
                </progress>
                <span data-file-upload-progress-text class=file-upload-progress-text>
                    wating to Upload
                </span>
            </div>
            <button data-file-uploading-cancel-button data-file-cancel-button-for="${file.name}" class="file-uploading-cancel-button" type="button">
                <span class="material-symbols-outlined text-accent-dark_gray hover:text-accent">
                    close
                </span>
            </button>
        </div>`

        const button = li.querySelector("[data-file-uploading-cancel-button]")
        button.addEventListener("click", () => {
            cancelFileUPload(file.name, li)
        })
        return li;
    }

    function addFileUploadingtoList(file) {
        const li = createFileUploadListItem(file)
        filesUploadingList.appendChild(li)
    }


    


    function uploadFile(file) {
        return new Promise((resolve, reject) => {
            const req = new XMLHttpRequest(); 
            currentReq = req
            currentFile = file
            req.open("POST", "/Instructor/UploadLessonVideo", true);
            const formData = new FormData();
            formData.append("file", file);

            req.onload = function () {
                if (this.status === 200) {
                    const response = JSON.parse(this.response);
                    replaceCancelButtonWithCheck(file);
                    const filesToUpload = fileHandler.getSelectedFiles();
                    if (filesToUpload.length === 1) {
                        updateVideoDetailsFormStatus(response.lessonId);
                        uploadedLessonId = response.lessonId;
                    }
                    resolve();
                } else {
                    console.log(this.response)
                    reject(new Error('Upload failed'));
                }
            };

            req.upload.onprogress = function (e) {
                if (e.lengthComputable) {
                    const progress = (e.loaded / e.total) * 100;
                    updateUploadProgress(progress, file);
                }
            };

            req.onerror = function () {
                console.log(this.response);
                reject(new Error('Upload failed'));
            };

            req.onabort = function () {
                reject(new Error('Upload was aborted')); 
            };

       
            req.send(formData);
        });
    }

    //function uploadFile(file) {
    //    const req = new XMLHttpRequest();
    //    req.open("POST", "/Instructor/UploadLessonVideo", true)
    //    const formData = new FormData();
    //    formData.append("file", file);
    //    currentReq = req;

    //    let successs = false;

    //    req.onload = function () {
    //        if (this.status === 200) {
    //            const response = JSON.parse(this.response)
    //            successs=true
    //            replaceCancelButtonWithCheck(file)
    //            const filesToUpload = fileHandler.getSelectedFiles();
    //            if (filesToUpload.length === 1) {
    //                updateVideoDetailsFormStatus(response.lessonId);
    //                uploadedLessonId = response.lessonId;
    //            }
    //        }
    //    }

    //    req.upload.onprogress = function (e) {
    //        if (e.lengthComputable) {
    //            const progress = (e.loaded / e.total) * 100;
    //            updateUploadProgress(progress, file)
               
    //        }
    //    };
        
    //    req.onerror = function () {
    //        console.log(this.response)
    //    }
    //    req.send(formData)

    //    return success;
    //}

    async function processQueue() {
        if (!uploadQueue.length || isUploading) return;

        isUploading = true;
        const file = uploadQueue[0];
        try {
            await uploadFile(file);
            uploadQueue.shift();
            uploadedFiles.push(file)
        } catch (error) {
            console.error('Upload failed:', error);
        }

        isUploading = false;
        processQueue(); 
    }



    function uploadFiles(files) {
        updateUIonUploadStart()
          for (let file of files) {
             addFileUploadingtoList(file)
        }
        uploadQueue.push(...files)
        processQueue();
      }

        
    if (uploadVideoButton) {
        uploadVideoButton.addEventListener("click", () => {
            const filesToUpload = fileHandler.getSelectedFiles();
            if (filesToUpload.length > 0) {
                alert("do not leave or refresh the page while video uploads go on")
                uploadFiles(filesToUpload)
            }    
        })
    }

    return {
        getUploadedLessonId: function () {
            return uploadedLessonId;
        },
        updateUiOnComplete: updateUiOnComplete
    }
})();

//IIFE
//Lesson Video Details Upload

const handleCompleteLesson = (function () {

    const completeVideoDetailsForm = document.querySelector("[data-video-details-form]")
    const completeVideoDetailsFormButton = document.querySelector("[data-complete-video-details-button]")

    function populateAndUpdateLessonErrorFields(errors) {
        const errorArray = Object.entries(errors)
        errorArray.forEach(([fieldName, fieldError]) => {
            const validationSpan = completeVideoDetailsForm.querySelector(`[data-lesson-details-validation-for="${fieldName}"]`)
            const validationInputContainer = completeVideoDetailsForm.querySelector(`[data-lesson-details-input-for="${fieldName}"]`)
            if (validationInputContainer && fieldError != "") {
                validationInputContainer.classList.add("input-error")
            }
            if (validationSpan && fieldError!="") {
                validationSpan.textContent=fieldError
            }
        
            if (validationInputContainer) {
                const input = validationInputContainer.children[0]
                if (input.type !== "file") {
                    input.addEventListener("input", (e) => {
                        if (fieldError != "") {
                            if (e.target.value.trim() === "") {
                                validationSpan.textContent = fieldError
                                validationInputContainer.classList.add("input-error")
                            } else {
                                validationSpan.textContent = ""
                                validationInputContainer.classList.remove("input-error")
                            }
                        }
                    })
                }
                if (input.type === "file") {
                    input.addEventListener("change", (e) => {
                        if (fieldError != "") {
                            if (!e.target.files || e.target.files.length === 0 || e.target.value=="") {
                                validationSpan.textContent = fieldError
                            } else {
                                validationSpan.textContent = ""
                            }
                        }
                    })

                }

            }
        })
    }

   
     function resetUi() {
        completeVideoDetailsForm.reset();
        completeVideoDetailsForm.action = ""
         completeVideoDetailsFormButton.disabled = true;
        completeVideoDetailsFormButton.classList.add("disabled")
        fileHandler.clearSelectedFiles();
        fileHandler.clearUi()
         uploadHandling.updateUiOnComplete()
        clearThumbnial()
    }
    
    

    function displayCompleteLessonDetialsError(response) {
        const modelOnlyErrorElement = completeVideoDetailsForm.querySelector("[data-lesson-details-validation-modelonly]")
       console.log(modelOnlyErrorElement)
       if (response.modelOnly && !response.success) {
           modelOnlyErrorElement.textContent= response.message
        }
        if (!response.ModelOnly && !response.success && response.errors) {
            populateAndUpdateLessonErrorFields(response.errors)
        }
    }
    function competeLessonDetials(lessonId) {
        const req = new XMLHttpRequest();
        req.open("POST", "/Instructor/CompleteLessonDetails?Id=" + encodeURIComponent(lessonId), true)
        const formData = new FormData(completeVideoDetailsForm);

        req.onload = function () {
            const response = JSON.parse(this.response)
            if (this.status == 200) {
                toastHandler.showToast("Succesfully completed Lesson details", "success")
                resetUi();
                
             
            } else {
                toastHandler.showToast("Failed complete Lesson details","error")
                displayCompleteLessonDetialsError(response)
            }
        }

        req.send(formData);

        req.onerror = function () {
            console.log(this.response)
        }
    };

    completeVideoDetailsFormButton.addEventListener("click", () => {
        const lessonId = uploadHandling.getUploadedLessonId();
        competeLessonDetials(lessonId)
    })

})();








