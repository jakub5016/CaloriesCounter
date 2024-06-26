import React, { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Button, IconButton, InputBase } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import fetchAllProducts from "./fetchAllProducts";
import handleSearch from "./handleSearch";
import ApiAdd from "./apiAdd/ApiAdd";
import HumanAdd from "./humanAdd/HumanAdd";
import deleteProduct from "./deletePoduct";
import RemoveIcon from '@mui/icons-material/Remove';


function appendMealList(id, data, amountArray, date=null, type=null){
  let promises = [];


  let ids = [];
  amountArray.forEach((amount, index) => {
    if (amount !== 0) {
      ids.push(data[index].id);
    }
  });
  
  let ammounts = amountArray.filter(amount => amount !== 0);


  console.log(JSON.stringify({
    type: type,
    date: date,
    productIds : ids,
    ammoutOfProduct :ammounts
  }))

  if ((id == "undefined") && ((date != null) && (type !=null))){
    console.log(JSON.stringify({
      type: type,
      date: date,
      productIds : ids,
      ammoutOfProduct :ammounts
    }))
    promises.push(fetch('https://localhost:7261/api/Meals',{
      method: "POST",
      body: JSON.stringify({
        type: type,
        date: date,
        productIds : ids,
        amountOfProduct :ammounts
      }),
      headers: {
        'accept': 'text/plain', 'Content-Type': 'application/json'
      }
    }))
  }
  else{
    amountArray.map((ammout, index)=>{
      if ((ammout != null) && (ammout != 0)){
        promises.push(fetch('https://localhost:7261/api/Meals/' + id + '/AppendProduct/' + data[index].id + "/" + ammout, {method: "PATCH"}))

      }
    })
  }

  return Promise.all(promises)
}

function AddToMeal() {
  const { id } = useParams();
  const { state } = useLocation();
  const [data, setData] = useState(null);
  const [amountArray, setAmountArray] = useState([]);
  const [openApi, setOpenApi] = useState(false)
  const [openHuman, setOpenHuman] = useState(false)
  const [textFiledColor,setTextFiledColor] = useState([])
  const navigate = useNavigate()
  
  useEffect(() => {
    fetchAllProducts()
      .then(data => {
        setData(data);
        setAmountArray(new Array(data.length).fill(0));
        setTextFiledColor(new Array(data.length).fill(""));
      })
      .catch(error => {
        console.error('Error fetching product data:', error);
      });
  }, []);
  

  const handleFillAmountArray = (index, value) => {
    setAmountArray(prevArray => {
      const newArray = [...prevArray];
      newArray[index] = value;
      return newArray;
    });
  };

  const handleDelete = async (id) =>{
    try {
      await deleteProduct(id);
      window.location.reload();
    } catch (error) {
      console.error('Error deleting product: ', error);
    }
  } 

  const handleSubmit = async () => {
    try {
      await (appendMealList(id, data, amountArray, state.date, state.type));
      navigate("/");
      window.location.reload();
    } catch (error) {
      console.error('Error appending meal list:', error);
    }
  };
  return (
    <div style={{display:"flex", flexDirection:"row"}}>

    {openHuman && <HumanAdd sx={{marginRight:"30px"}} setOpenHuman={setOpenHuman}/>}
    {!openHuman && <Button sx={{fontSize:"40px", border:"1px solid"}} onClick={()=>setOpenHuman(!openApi)}>.<br/>.<br/>.<br/></Button>}

    <Paper style={{display:"flex", flexDirection:"column", alignItems:"center"}}>
      <h1>Add To Meal</h1>
      {/* <p>Meal ID: {id}</p> */}
      <div style={{display:"flex", justifyContent:"center"}}>
        <SearchIcon />
        <InputBase sx= {{marginLeft: "3px"}} placeholder="Wyszukaj" onChange={e => handlehSearch(e.target.value, setData, setAmountArray)}/>
      </div>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell align="left" sx={{ fontWeight: 'bold' }}>Name</TableCell>
              <TableCell align="left" sx={{ fontWeight: 'bold' }}>Calories</TableCell>
              <TableCell align="left" sx={{ fontWeight: 'bold' }}>Fat&nbsp;(g)</TableCell>
              <TableCell align="left" sx={{ fontWeight: 'bold' }}>Carbs&nbsp;(g)</TableCell>
              <TableCell align="left" sx={{ fontWeight: 'bold' }}>Protein&nbsp;(g)</TableCell>
              <TableCell align="left" sx={{ fontWeight: 'bold' }}>Amount</TableCell>
              <TableCell align="left" sx={{ fontWeight: 'bold', borderBottom: "none"}}></TableCell>
              
            </TableRow>
          </TableHead>
          <TableBody>
            {data && data.map((product, index) => (
              <TableRow key={product.name} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                <TableCell component="th" scope="row">{product.name}</TableCell>
                <TableCell align="left">{product.kcal}</TableCell>
                <TableCell align="left">{product.fat}</TableCell>
                <TableCell align="left">{product.carbs}</TableCell>
                <TableCell align="left" >{product.protein}</TableCell>
                <TableCell sx={{backgroundColor:textFiledColor[index]}}>
                  <input 
                    type="number" 
                    value={amountArray[index]} 
                    onChange={e => {if (!isNaN(parseInt(e.target.value))) {
                                      handleFillAmountArray(index, parseInt(e.target.value));
                                      setTextFiledColor(textFiledColor.map((val, colorIndex) =>{if (index == colorIndex){return ""} else{return val}}));
                                    } else {
                                      handleFillAmountArray(index, parseInt(e.target.value));
                                      setTextFiledColor(textFiledColor.map((val, colorIndex) =>{if (index == colorIndex){return "red"} else{return val}}));
                                    }}}
                  />
                </TableCell>
                <TableCell align="left" ><IconButton 
                      style={{border:"1px solid", color:"primary", marginRight: "0px", backgroundColor:"white"}}
                      onClick={()=>{handleDelete(product.id)}}
                      ><RemoveIcon/></IconButton></TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <Button onClick={handleSubmit}>Submit</Button>
    </Paper>
    
    {openApi && <ApiAdd setOpenApi={setOpenApi}/>}
    {!openApi && <Button sx={{fontSize:"40px", border:"1px solid"}} onClick={()=>setOpenApi(!openApi)}>.<br/>.<br/>.<br/></Button>}

    </div>
  );
}

export default AddToMeal;
