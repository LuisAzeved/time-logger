import React, { useState } from "react";
import CustomTable, { Column, SortField } from "../components/CustomTable";
import useGetProjects from "../api/hooks/useGetProjects";
import { Project, SortCriteria } from "../utils/types";
import ProjectDialog from "./ProjectDialog";
import { Chip, Typography } from "@mui/material";
import moment from "moment";
import styled from "styled-components";

const MainContainer = styled.div`
  padding: 1rem;
`;

const TitleContainer = styled.div`
  margin: 1rem 0 2rem 0;
`;

const defaultColumns: Column[] = [
  {
    fieldName: "id",
    headerName: "#",
    width: 300,
  },
  {
    fieldName: "name",
    headerName: "Name",
    width: 300,
  },
  {
    fieldName: "deadline",
    headerName: "Deadline",
    width: 300,
  },
  {
    fieldName: "status",
    headerName: "Status",
    width: 300,
    notSortable: true,
  },
];

export default function Projects() {
  const [sortField, setSortField] = useState<SortField | undefined>(undefined);

  const [dialogOpen, setDialogOpen] = useState(false);
  const [dialogProject, setDialogProject] = useState<Project | undefined>(
    undefined
  );

  const columns = defaultColumns.map((column) => ({
    ...column,
    onSort: column.notSortable
      ? undefined
      : () => {
          const clickingOnSameFieldAgain =
            sortField?.fieldName === column.fieldName;
          let criteria: SortCriteria;
          if (clickingOnSameFieldAgain) {
            criteria =
              sortField.criteria === SortCriteria.ASC
                ? SortCriteria.DESC
                : SortCriteria.ASC;
          } else {
            criteria = SortCriteria.DESC;
          }

          setSortField({
            fieldName: column.fieldName,
            criteria,
          });
        },
  }));

  const { data: projects, isLoading } = useGetProjects({
    sortCriteria: sortField?.criteria,
    sortFieldName: sortField?.fieldName,
  });

  return (
    <MainContainer>
      <TitleContainer>
        <Typography variant="h5">Your projects</Typography>
      </TitleContainer>
      <CustomTable
        rows={
          projects?.map((project) => {
            const deadlineOver = moment(project?.deadline).isBefore(new Date());
            return {
              ...project,
              status: (
                <Chip
                  label={deadlineOver ? "Completed" : "In progress"}
                  color={deadlineOver ? "success" : "info"}
                />
              ),
            };
          }) ?? []
        }
        columns={columns}
        sortField={sortField}
        loading={isLoading}
        onRowClick={(project: Project) => {
          setDialogOpen(true);
          setDialogProject(project);
        }}
      />
      {dialogProject ? (
        <ProjectDialog
          open={dialogOpen}
          setOpen={setDialogOpen}
          projectId={dialogProject.id}
          onClose={() => {
            setDialogProject(undefined);
          }}
        />
      ) : (
        ""
      )}
    </MainContainer>
  );
}
