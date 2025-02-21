import { useState } from "react";
import * as React from "react";
import AppBar from "../Components/AppBar/AppBar";
import "./App.css";
import ButtonAppBar from "../Components/AppBar/AppBar";
import Table from "../Components/Table/TableContact";
import NavBar from "../Components/Drawer/Drawer";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import MainPage from "./MainPage";
import Contact from "./Contact";
import Customer from "./Customer";
import Employee from "./Employee";
import Service from "./Service";
import Project from "./Project";
import Role from "./Role";

function App() {
  return (
    <div className="appBar_flex">
      <div className="appBar">
        <BrowserRouter>
          <NavBar />
          <Routes>
            <Route path="/" element={<MainPage />} />
            <Route path="/Contact" element={<Contact />} />
            <Route path="/Customer" element={<Customer />} />
            <Route path="/Employee" element={<Employee />} />
            <Route path="/Service" element={<Service />} />
            <Route path="/Project" element={<Project />} />
            <Route path="/Role" element={<Role />} />
          </Routes>
        </BrowserRouter>
      </div>
    </div>
  );
}

export default App;
