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
            uploadingFilesContainer.classList.add("md:w-1/2", "self-center", "items-center", "justify-center")
        }

        if (selectedFiles.length === 1) {
            videoFileDropZone.classList.add("hidden")
            uploadingFilesContainer.classList.remove("hidden")
            uploadingFilesContainer.classList.add("flex")
            lessonDetailsForm.classList.remove("hidden")
            uploadingFilesContainer.classList.remove("md:w-1/2", "self-center", "items-center", "justify-center")
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

//actual vidoe uploading logic
const uploadVideoButton = document.querySelector("[data-files-upload-button]")

uploadVideoButton.addEventListener("click", () => {
    alert("do not leave or refresh the page while video uploads go on")
})









