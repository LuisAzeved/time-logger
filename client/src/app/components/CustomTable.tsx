import React from "react";
import {
  TableBody,
  TableCell,
  TableRow,
  TableHead,
  Table,
  TableSortLabel,
  CircularProgress,
} from "@mui/material";
import { SortCriteria } from "../utils/types";
import styled from "styled-components";

export type Row = {
  id: number;
  [fieldName: string]: any;
};

export type Column = {
  fieldName: string;
  headerName: string;
  width: number;
  onSort?: () => void;
  notSortable?: boolean;
};

export type SortField = {
  fieldName: string;
  criteria: SortCriteria;
};

const ClickableTableCell = styled(TableCell)<{ $clickable: boolean }>`
  ${(props) => (props.$clickable ? "cursor: pointer;" : "")}
`;

const StyledTable = styled(Table)`
  width: 100%;
`;

const StyledTableRow = styled(TableRow)<{ $clickable: boolean }>`
  ${(props) => (props.$clickable ? "cursor: pointer;" : "")}
`;

const CircularProgressContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  width: 100%;
`;

const StyledCircularProgress = styled(CircularProgress)``;

export default function CustomTable<T extends Row>({
  rows,
  columns,
  sortField,
  loading,
  onRowClick,
  size = "medium",
}: {
  rows: T[];
  columns: Column[];
  sortField?: SortField;
  loading?: boolean;
  onRowClick?: (object: T) => void;
  size?: "small" | "medium";
}) {
  return (
    <StyledTable size={size}>
      <TableHead>
        <TableRow>
          {columns.map((column) => {
            const tableCell =
              column.fieldName === sortField?.fieldName ? (
                <TableSortLabel
                  active={true}
                  direction={
                    sortField.criteria === SortCriteria.NONE
                      ? undefined
                      : (sortField.criteria?.toLowerCase() as "asc" | "desc")
                  }
                >
                  {column.headerName}
                </TableSortLabel>
              ) : (
                column.headerName
              );

            return (
              <ClickableTableCell
                key={column.fieldName}
                width={column.width}
                height={60}
                onClick={column.onSort}
                $clickable={!!column.onSort}
              >
                {tableCell}
              </ClickableTableCell>
            );
          })}
        </TableRow>
      </TableHead>

      <TableBody>
        {loading ? (
          <CircularProgressContainer>
            <StyledCircularProgress data-testid={"circular-progress"} />
          </CircularProgressContainer>
        ) : (
          <>
            {rows.map((row) => (
              <StyledTableRow
                hover
                $clickable={!!onRowClick}
                key={row.id}
                onClick={() => onRowClick?.(row)}
              >
                {columns.map((column) => (
                  <TableCell width={column.width} key={column.fieldName}>
                    {row[column.fieldName]}
                  </TableCell>
                ))}
              </StyledTableRow>
            ))}
          </>
        )}
      </TableBody>
    </StyledTable>
  );
}
