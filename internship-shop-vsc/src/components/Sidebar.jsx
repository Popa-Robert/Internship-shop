import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Nav } from "react-bootstrap";
import ApiHelper from "../helpers/api";
import "../style/sidebar.css";

const Sidebar = (props) => {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    ApiHelper.Categories.all().then((mappedApiCategories) => {
      setCategories(mappedApiCategories);
    });
    let newobject = [{id:4}, {id:5}];
    for (var i=0; i<10; i++)
    {
      console.log("i1= ",i);
      for (var i=10; i<20; i++)
      {
        console.log("i2= ",i);
      }
    }
    return () => {
      newobject = [];
    }
  }, [props]);


  return (
    <Nav className="d-md-block bg-light sidebar">
      {categories.map((c) => (
        <Nav.Item key={c.categoryId}>
          <Nav.Link as={Link} to={`/products/${c.categoryId}`}>
            {c.name}
          </Nav.Link>
        </Nav.Item>
      ))}
    </Nav>
  );
};
export default Sidebar;