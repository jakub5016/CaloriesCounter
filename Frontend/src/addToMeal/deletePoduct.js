async function deleteProduct(id){
  await fetch("https://localhost:7261/api/Product/" + id, {method: "DELETE"})
        .then(
            resp=>{
                if (!resp.ok){
                    console.error("Something went bad in delete function");
                }
            }
        )
        .catch(
            msg=>{
                console.error('Error deleting product:', msg);
            }
        )
}

export default deleteProduct