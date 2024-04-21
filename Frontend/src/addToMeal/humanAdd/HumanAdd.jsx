import { Box, Button, IconButton, Paper } from "@mui/material"
import { useState } from "react"


function addProduct(productObject){
    console.log(productObject)
    let isConrrect = true
    if (productObject != null){
        if (productObject.name == ""){isConrrect = false} 
        if (productObject.kcal == 0){isConrrect = false} 

        if (isConrrect == false){return false}

        return fetch('https://localhost:7261/api/Product',{
            method: "POST",
            body: JSON.stringify(productObject),
            headers: {
              'accept': 'text/plain', 'Content-Type': 'application/json'
            }
          })
        
    }
    else{
        return false
    }

}

function HumanAdd(props){
    const [name, setName] = useState("")
    const [kcal, setKcal] = useState(0)
    const [protein, setProtein] = useState(0)
    const [fat, setFat] = useState(0)
    const [carbs, setCarbs] = useState(0)


    return(
        <Paper sx={{marginLeft:"30px", padding:"7px", textAlign:"left"}}>
            <IconButton onClick={()=>{props.setOpenHuman(false)}}>X</IconButton><br/>
            <Box sx={{textAlign:"center"}}>
                <h3>Dodaj własny produkt</h3>
                <form style={{display: 'flex', flexDirection:"column", textAlign:"left"}}>
                    <label>
                        Nazwa Produktu: 
                        <input type="text" style={{marginLeft:"5px"}} onChange={(e)=>{setName(e.target.value)}}></input>
                    </label>
                    <label>
                        Kilokalorie na 100 g: 
                        <input type="text" style={{marginLeft:"5px"}} onChange={(e)=>{setKcal(e.target.value)}}></input>
                    </label>
                    <label>
                        Białko na 100g: 
                        <input type="text" style={{marginLeft:"5px"}} onChange={(e)=>{setProtein(e.target.value)}}></input>
                    </label>
                    <label>
                        Tłuszcze na 100g: 
                        <input type="text" style={{marginLeft:"5px"}} onChange={(e)=>{setFat(e.target.value)}}></input>
                    </label>
                    <label>
                        Węglowodany na 100g: 
                        <input type="text" style={{marginLeft:"5px"}} onChange={(e)=>{setCarbs(e.target.value)}}></input>
                    </label>

                    <Button variant="outlined" onClick={()=>
                        {
                            addProduct({
                                name: name,
                                kcal: kcal,
                                protein: protein,
                                fat: fat,
                                carbs: carbs
                            }); window.location.reload()
                        }
                        }>Dodaj produkt</Button>
                </form>
            </Box>            

        </Paper>
    )
}

export default HumanAdd