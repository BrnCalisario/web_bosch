$(document).ready(function () {
    let photo = document.getElementById('imgFoto')
    let file = document.getElementById('flImage')

    photo.addEventListener('click', () => {
        file.click()
    })

    file.addEventListener('change', () => {
        if (file.files.length == 0)
            return
    
        let reader = new FileReader()
    
        reader.readAsDataURL(file.files[0])
    
        reader.onload = () => {
            photo.src = reader.result
        }
    })
})

