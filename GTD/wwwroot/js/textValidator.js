// agradece Yui :3
function validateDays(text) {
    let isText = false;
    let currDay = "";
    let currText = "";
    let data = [];
    let textArray = text.split("\n");

    textArray.forEach(line => {
        if (isText) {
            currText += line;
        }

        if (line === "Domingo" ||
            line === "Segunda-feira" ||
            line === "Terça-feira" ||
            line === "Quarta-feira" ||
            line === "Quinta-feira" ||
            line === "Sexta-feira" ||
            line === "Sábado") {
            currDay = line;
            isText = true;
        }

        if (line === "") {
            if (currDay !== "" && currText !== "") {
                data.push({
                    day: currDay,
                    text: currText
                });
                currDay = "";
                currText = "";
            }
            isText = false;
        }
    });

    if (Object.keys(data).length !== 6) {
        return false;
    }
    else {
        return true;
    }
}