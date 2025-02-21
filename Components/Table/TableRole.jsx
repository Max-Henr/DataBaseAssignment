import * as React from "react";
import GenericTable from "./GenericTable";

function TableRole() {
  const apiEndpoint = "https://localhost:7139/api/Role";
  const columns = [
    { field: "roleName", headerName: "Role" },
  ];
  const inputFields = [
    { name: "roleName", label: "Role" },
  ];

  return (
    <GenericTable
      apiEndpoint={apiEndpoint}
      columns={columns}
      inputFields={inputFields}
      buttonText="Add Role"
    />
  );
}

export default TableRole;