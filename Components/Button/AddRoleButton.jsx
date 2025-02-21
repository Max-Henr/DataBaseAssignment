import * as React from "react";
import Button from "@mui/material/Button";

export default function AddRoleButton({ onClick }) {
  return (
    <Button variant="contained" color="primary" onClick={onClick} sx={{ m: 1 }}>
      Add New Role
    </Button>
  );
}
