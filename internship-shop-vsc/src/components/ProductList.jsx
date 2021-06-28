import React, { useState } from "react";
import ApiHelper from "../helpers/api"

const ProductList = props => {
    const [products,setProducts] = useState([]);
    ApiHelper.ProductsProv.all().then((x)=>{
        setProducts(x);
    })
    return (<div>
        {products.map((x)=>{return (
        <span>{x.name}</span>
    )})
    }
    </div>);
};

export default ProductList;