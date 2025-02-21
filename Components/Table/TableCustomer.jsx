import * as React from "react";
import GenericTable from "./GenericTable";

function TableCustomer() {
  const apiEndpoint = "https://localhost:7139/api/customer";
  const dropDownApiEndpoints = {
    contactId: "https://localhost:7139/api/contact",
  };
  const columns = [
    { field: "name", headerName: "Customer Name" },
    { field: "contact.firstName", headerName: "Contact First Name" },
    { field: "contact.lastName", headerName: "Contact Last Name" },
    { field: "contact.email", headerName: "Contact Email" },
    { field: "contact.phoneNumber", headerName: "Contact Phone Number" },
  ];
  const inputFields = [
    { name: "name", label: "Customer Name" },
    { name: "contactId", label: "Contact Person ID", dropdown: true },
  ];
  const displayProperties = {
    contactId: "firstName",
  };

  return (
    <GenericTable
      apiEndpoint={apiEndpoint}
      columns={columns}
      inputFields={inputFields}
      displayProperties={displayProperties}
      dropDownApiEndpoints={dropDownApiEndpoints}
      buttonText="Add Customer"
    />
  );
}

export default TableCustomer;
