import axios from "axios";

const API = axios.create({
  baseURL: "https://localhost:7220/",
  headers: {
    "Content-Type": "application/json",
  },
})

export default API;