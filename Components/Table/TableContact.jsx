import * as React from "react";
import GenericTable from "./GenericTable";

function TableContact() {
  const apiEndpoint = "https://localhost:7139/api/contact";
  const columns = [
    { field: "firstName", headerName: "First Name" },
    { field: "lastName", headerName: "Last Name" },
    { field: "email", headerName: "Email" },
    { field: "phoneNumber", headerName: "Phone Number" },
  ];
  const inputFields = [
    { name: "firstName", label: "First Name" },
    { name: "lastName", label: "Last Name" },
    { name: "email", label: "Email" },
    { name: "phoneNumber", label: "Phone Number" },
  ];

  return (
    <GenericTable
      apiEndpoint={apiEndpoint}
      columns={columns}
      inputFields={inputFields}
      buttonText="Add Contact"
    />
  );
}
export default TableContact;
