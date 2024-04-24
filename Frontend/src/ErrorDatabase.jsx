import { Paper } from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

async function checkIfDataAvailable() {
    try {
        const response = await fetch("https://localhost:7261/api/User", { method: "GET", headers: { "accept": "text/plain" } });
        return response.ok; // Return true if response status is in the range 200-299
    } catch (error) {
        return false; // Return false if there's an error
    }
}

function ErrorDatabase() {
    const [noError, setNoError] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            const isDataAvailable = await checkIfDataAvailable();
            setNoError(isDataAvailable);
        };
        fetchData();
    }, []);

    const navigate = useNavigate();

    return (
        noError ?
            navigate("/")
            :
            <Paper sx={{ textAlign: "center", margin: "20px" }}>
                <h1>
                    Połączenie nieudane, nie można pobrać danych na temat użytkownika
                </h1>
                <h2>
                    Czy sesja backendowa napewno działa na odpowiednim porcie?
                </h2>
            </Paper>
    );
}

export default ErrorDatabase;
