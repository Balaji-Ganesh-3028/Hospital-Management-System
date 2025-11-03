// src/components/GlobalSpinner.jsx
import React from "react";
import { RingLoader } from "react-spinners";
import { useSpinner } from "../../Contexts/SpinnerContext";

const GlobalSpinner = () => {
  const { loading } = useSpinner();

  if (!loading) return null;

  return (
    <div
      style={{
        position: "fixed",
        top: 0,
        left: 0,
        width: "100%",
        height: "100%",
        backgroundColor: "rgba(0, 0, 0, 0.5)",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        zIndex: 9999,
      }}
    >
      <RingLoader
        color="#36d7b7"
        loading={loading}
        size={60}
        aria-label="Loading Spinner"
      />
    </div>
  );
};

export default GlobalSpinner;
