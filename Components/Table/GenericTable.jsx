import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useEffect, useState } from "react";
import ButtonContact from "../Button/Button";
import InputField from "../InputField/InputField";
import DeleteButton from "../DeleteButton/DeleteButton";
import Button from "@mui/material/Button";
import EditIcon from "@mui/icons-material/Edit";
import { format, parseISO } from "date-fns";

function GenericTable({
  apiEndpoint,
  columns,
  inputFields,
  displayProperties,
  dropDownApiEndpoints,
  buttonText,
}) {
  const [data, setData] = useState([]);
  const [showInputFields, setShowInputFields] = useState(false);
  const [editItem, setEditItem] = useState(null);

  const fetchData = async () => {
    try {
      const res = await fetch(apiEndpoint);
      const result = await res.json();
      setData(result);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    fetchData();
  }, [apiEndpoint]);

  const handleButtonClick = () => {
    setShowInputFields(!showInputFields);
    setEditItem(null);
  };

  const handleEdit = (item) => {
    setShowInputFields(true);
    setEditItem(item);
  };

  const handleDelete = async (id) => {
    try {
      const res = await fetch(`${apiEndpoint}/${id}`, {
        method: "DELETE",
      });
      if (!res.ok) {
        throw new Error("Network response was not ok");
      }
      setData(data.filter((item) => item.id !== id));
      fetchData();
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const formatDate = (dateString) => {
    try {
      const date = parseISO(dateString);
      return format(new Date(date), "yyyy-MM-dd");
    } catch (error) {
      console.error("Invalid date format:", dateString);
      return dateString;
    }
  };

  const statusMapping = {
    0: "Pending",
    1: "Active",
    2: "Completed",
    3: "Cancelled",
  };

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            {columns.map((column) => (
              <TableCell key={column.field} align={column.align || "left"}>
                {column.headerName}
              </TableCell>
            ))}
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((item) => (
            <TableRow
              key={item.id}
              sx={{ "&:last-child td, &:last-child th": { border: 0 } }}>
              {columns.map((column) => {
                let value = column.field
                  .split(".")
                  .reduce((o, i) => (o ? o[i] : undefined), item);
                if (column.type === "date" && value) {
                  value = formatDate(value);
                }
                if (column.field === "status" && value !== undefined) {
                  value = statusMapping[value];
                }
                return (
                  <TableCell key={column.field} align={column.align || "left"}>
                    {value !== undefined ? value : ""}
                  </TableCell>
                );
              })}
              <TableCell>
                <Button
                  startIcon={<EditIcon />}
                  onClick={() => handleEdit(item)}>
                  Edit
                </Button>
                <DeleteButton onClick={() => handleDelete(item.id)} />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
      <ButtonContact buttonText={buttonText} onClick={handleButtonClick} />
      {showInputFields && (
        <InputField
          apiEndpoint={apiEndpoint}
          fields={inputFields}
          fetchData={fetchData}
          displayProperties={displayProperties} // Pass displayProperties prop here
          dropDownApiEndpoints={dropDownApiEndpoints} // Pass dropDownApiEndpoints prop here
          editItem={editItem} // Pass editItem prop here
        />
      )}
    </TableContainer>
  );
}

export default GenericTable;
