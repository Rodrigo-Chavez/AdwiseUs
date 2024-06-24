

let currentMarkerPosition = 0;
const moveClasses = [
  "0", "100", "200", "300", "400", "500", "600"
].map(position => `move-to-${position}`);


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



document.addEventListener('DOMContentLoaded', () => {
    const leftButton = document.querySelector(".left-arrow-button");
    const rightButton = document.querySelector(".right-arrow-button");
    const timeLineYearMarker = Array.from(document.querySelectorAll(".year-marker"));
    const seeMoreButtons = document.querySelectorAll('.see-more');
    const displayText = document.getElementById('display-text');
    
    function moveAllMarkers(amount) {
        return () => {
            currentMarkerPosition = Math.max((amount + currentMarkerPosition) % moveClasses.length, 0)
            const newMovePositionClass = moveClasses[currentMarkerPosition]

            timeLineYearMarker.forEach(marker => {
                const isUp = marker.className.includes("up")
                const directionClass = (isUp) ? "up" : "down"
                marker.className = `year-marker ${directionClass} ${newMovePositionClass}`
            })
        }
    }

    rightButton.addEventListener("click", moveAllMarkers(1))
    leftButton.addEventListener("click", moveAllMarkers(-1))


    console.log(urlInfo.value)
    
    

    seeMoreButtons.forEach(button => {
        button.addEventListener('click', () => {
            const year = button.getAttribute('data-year');
           
            $.ajax({
                url: '/Home/GetTimelineResource',
                type: 'GET',
                data: { year: year, url: urlInfo.value },
                success: function (data) {
                    console.log(data)
                    displayText.innerHTML = '';

                    const h2 = document.createElement('h2');
                    h2.textContent = data.h2;
                    displayText.appendChild(h2);

                    const h3 = document.createElement('h3');
                    h3.textContent = data.h3;
                    displayText.appendChild(h3);

                    const p = document.createElement('p');
                    p.textContent = data.text;
                    displayText.appendChild(p);

                    displayText.style.display = 'block'; 
                },
                error: function (error) {
                    displayText.innerHTML = "<p>No detailed information available for this year</p>";
                    displayText.style.display = 'block'; 
                }
            });
            

        });
    });
});

