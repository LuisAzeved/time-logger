import React from "react";
import { render, fireEvent } from "@testing-library/react";
import CustomTable from "../../app/components/CustomTable";
import "@testing-library/jest-dom";

describe("CustomTable", () => {
  test("renders without crashing", () => {
    render(<CustomTable rows={[]} columns={[]} />);
  });

  test("shows CircularProgress when loading is true", () => {
    const { getByTestId } = render(
      <CustomTable rows={[]} columns={[]} loading={true} />
    );
    expect(getByTestId("circular-progress")).toBeInTheDocument();
  });

  test("calls onRowClick when a row is clicked", () => {
    const mockOnClick = jest.fn();
    const mockRow = { id: 1, fieldName: "test" };
    const { getByText } = render(
      <CustomTable
        rows={[mockRow]}
        columns={[{ fieldName: "fieldName", headerName: "Header", width: 100 }]}
        onRowClick={mockOnClick}
      />
    );
    fireEvent.click(getByText("test"));
    expect(mockOnClick).toHaveBeenCalledWith(mockRow);
  });

  test("renders correct number of rows and columns", () => {
    const rows = [
      { id: 1, name: "John", age: 25 },
      { id: 2, name: "Doe", age: 30 },
    ];
    const columns = [
      { fieldName: "name", headerName: "Name", width: 100 },
      { fieldName: "age", headerName: "Age", width: 50 },
    ];
    const { getAllByRole } = render(
      <CustomTable rows={rows} columns={columns} />
    );
    expect(getAllByRole("row")).toHaveLength(3); // header row + two rows;
    expect(getAllByRole("columnheader")).toHaveLength(2);
  });
});
