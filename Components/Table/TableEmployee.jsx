import * as React from "react";
import GenericTable from "./GenericTable";

function TableEmployee() {
  const apiEndpoint = "https://localhost:7139/api/employee";
  const dropDownApiEndpoints = {
    roleId: "https://localhost:7139/api/role",
  }; 

  const columns = [
    { field: "firstName", headerName: "First Name" },
    { field: "lastName", headerName: "Last Name" },
    { field: "role.roleName", headerName: "Role" },
  ];
  const inputFields = [
    { name: "firstName", label: "First Name" },
    { name: "lastName", label: "Last Name" },
    { name: "roleId", label: "Role", dropdown: true }, 
  ];

  const displayProperties = {
    roleId: "roleName",
  };

  return (
    <GenericTable
      apiEndpoint={apiEndpoint}
      columns={columns}
      inputFields={inputFields}
      displayProperties={displayProperties} 
      dropDownApiEndpoints={dropDownApiEndpoints} 
      buttonText="Add Employee" 
    />
  );
}

export default TableEmployee;
