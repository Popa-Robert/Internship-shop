import { API_ROUTE } from "../config/api";
import Category from "../models/category";
import Product from "../models/product";

const ApiClient = {
  get: (url, headers) => {
    console.log(url);
    return ApiClient.makeRequest(`${API_ROUTE}/${url}`, "GET", {}, headers);
  },

  makeRequest: async (url, type, params = {}, headers = {}) => {
    try {
      type = type.toUpperCase();
      const request = {
        method: type,
        headers: headers,
      };
      if (type === "POST" || type === "PUT") {
        request.body = JSON.stringify(params);
      }

      const result = await fetch(url, request);
      return await result.json();
    } catch (error) {
      console.log(error.message);
    }
  },
};

const Categories = {
  all: async () => {
    const { categories } = await ApiClient.get("category");

    return categories.map(
      (c) => new Category(c.categoryId, c.name, c.description)
    );
  }
};

const ProductsProv = {

  all:
    async () => {
      console.log("url2");
      const { products } = await ApiClient.get("product");

      return products.map(
        (p) => new Product(p.productId, p.name)
      );
    }
};

const ApiHelper = {
  Categories,
  ProductsProv,
};
export default ApiHelper;
