
const getValueInput = () => {
    let inputValue = document.getElementById("checkNotify").value;
    document.getElementById("valueInput").innerHTML = inputValue;

    if (inputValue == "1") {

        $(document).ready(function () {
            $('#a').after(function () {
                $('#liveAlert').show('fade');
                setTimeout(function () {
                    $('#liveAlert').hide('fade')
                }, 30000);


            });

            $('#linkclose').click(function () {
                $('#liveAlert').hide('fade');
            });
        });
    }
}

