import axios from "axios";

const API = axios.create({
  baseURL: "https://localhost:7220/",
  headers: {
    "Content-Type": "application/json",
    "Authorization": `Bearer ${localStorage.getItem("token")}`, 
  },
})

export default API;