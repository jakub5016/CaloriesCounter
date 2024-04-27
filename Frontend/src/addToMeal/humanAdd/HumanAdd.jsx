import { Box, Button, IconButton, Paper } from "@mui/material"
import { useEffect, useState } from "react"


function addProduct(productObject){
    console.log(productObject)
    let isConrrect = true
    if (productObject != null){
        if (productObject.name == ""){isConrrect = false} 
        if (productObject.kcal == 0){isConrrect = false} 

        if (isConrrect == false){ alert("Produkt nie może mieć 0 kcal!"); return false}

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
    const [bgColorArray, setBgColorArray] = useState([])

    useEffect(()=>{
        setBgColorArray(new Array(5).fill(""))
    },[])

    async function handleAdd(productGot){
        let status = await addProduct({
            name: name,
            kcal: kcal,
            protein: protein,
            fat: fat,
            carbs: carbs
        })
        if (status != false){
            window.location.reload()
        }
    }

    return(
        <Paper sx={{marginLeft:"30px", padding:"7px", textAlign:"left"}}>
            <IconButton onClick={()=>{props.setOpenHuman(false)}}>X</IconButton><br/>
            <Box sx={{textAlign:"center"}}>
                <h3>Dodaj własny produkt</h3>
                <form style={{display: 'flex', flexDirection:"column", textAlign:"left"}}>
                    <label style={{color: bgColorArray[0]}}>
                        Nazwa Produktu: 
                        <input type="text" style={{marginLeft:"5px"}} onChange={(e)=>{setName(e.target.value)}}></input>
                    </label>
                    <label style={{color: bgColorArray[1]}}>
                        Kilokalorie na 100 g: 
                        <input type="number" style={{marginLeft:"5px"}} onChange={(e)=>{
                            setKcal(e.target.value)
                            if (!Number.isInteger(parseInt(e.target.value))){
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 1) {return "red"} else{return val}}))
                                console.log(bgColorArray)
                            }
                            else{
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 1) {return ""} else{return val}}))
                            }                        
                        }}></input>
                    </label>
                    <label style={{color: bgColorArray[2]}}> 
                        Białko na 100g: 
                        <input type="number" style={{marginLeft:"5px"}} onChange={(e)=>{
                            setProtein(e.target.value)
                            if (!Number.isInteger(parseInt(e.target.value))){
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 2) {return "red"} else{return val}}))
                                console.log(bgColorArray)
                            }
                            else{
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 2) {return ""} else{return val}}))
                            }                 
                        }}></input>
                    </label>
                    <label style={{color: bgColorArray[3]}}>
                        Tłuszcze na 100g: 
                        <input type="number" style={{marginLeft:"5px"}} onChange={(e)=>{
                            setFat(e.target.value)
                            if (!Number.isInteger(parseInt(e.target.value))){
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 3) {return "red"} else{return val}}))
                                console.log(bgColorArray)
                            }
                            else{
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 3) {return ""} else{return val}}))
                            }                 
                        }}></input>
                    </label>
                    <label style={{color: bgColorArray[4]}}>
                        Węglowodany na 100g: 
                        <input type="number" style={{marginLeft:"5px"}} onChange={(e)=>{
                            setCarbs(e.target.value)
                            if (!Number.isInteger(parseInt(e.target.value))){
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 4) {return "red"} else{return val}}))
                                console.log(bgColorArray)
                            }
                            else{
                                setBgColorArray(bgColorArray.map((val, index)=>{ if (index == 4) {return ""} else{return val}}))
                            }                 
                        }}></input>
                    </label>

                    <Button variant="outlined" onClick={()=>
                        {
                            handleAdd({
                                name: name,
                                kcal: kcal,
                                protein: protein,
                                fat: fat,
                                carbs: carbs
                            });
                        }
                        }>Dodaj produkt</Button>
                </form>
            </Box>            

        </Paper>
    )
}

export default HumanAdd