

function toggleList(listId, imageTriangle) {
    var list = document.getElementById(listId);
    var image = document.getElementById(imageTriangle);
    if (list.style.display === "none") {
        list.style.display = "block";
        image.style.transform = 'rotate(90deg)'
    } else {
        list.style.display = "none";
        image.style.transform = 'rotate(0deg)'
    }
}