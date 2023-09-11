import {
  Button,
  ClickAwayListener,
  Dialog,
  Divider,
  Typography,
} from "@mui/material";
import React, {
  Dispatch,
  SetStateAction,
  useEffect,
  useMemo,
  useState,
} from "react";
import styled from "styled-components";
import CustomTable, { Column } from "../components/CustomTable";
import { DateTimePicker } from "@mui/x-date-pickers";
import useCreateTimeRegistration from "../api/hooks/useCreateTimeRegistration";
import useGetProject from "../api/hooks/useGetProject";
import moment, { Moment } from "moment";

const columns: Column[] = [
  {
    fieldName: "start",
    headerName: "Start",
    width: 100,
  },
  {
    fieldName: "end",
    headerName: "End",
    width: 100,
  },
  {
    fieldName: "time",
    headerName: "Time",
    width: 100,
  },
];

const DialogContainer = styled.div`
  padding: 1.5rem;
  height: 30rem;
`;

const YourEntriesContainer = styled.div`
  max-height: 55%;
  overflow: scroll;
  margin-top: 0.5rem;
`;

const TableContainer = styled.div`
  margin: 1rem 1rem 0 0;
  height: 70%;
  width: 80%;
`;

const AddEntryContainer = styled.div`
  margin-top: 2rem;
  margin-bottom: 1.5rem;
`;

const AddEntryFormContainer = styled.div`
  display: flex;
  gap: 1rem;
  margin-top: 1rem;
`;

export default function ProjectDialog({
  open,
  setOpen,
  projectId,
  onClose,
}: {
  open: boolean;
  setOpen: Dispatch<SetStateAction<boolean>>;
  projectId: number;
  onClose: () => void;
}) {
  const [startTime, setStartTime] = useState<Moment | null>(null);
  const [endTime, setEndTime] = useState<Moment | null>(null);
  const [createTimeRegistrationEnabled, setTimeRegistrationEnabled] =
    useState(false);

  const createTimeRegistrationMutation = useCreateTimeRegistration();

  const { data: project, refetch: refetchProject } = useGetProject(
    { projectId },
    { enabled: !!projectId }
  );

  const deadlineOver = moment(project?.deadline).isBefore(new Date());

  useEffect(() => {
    if (!!startTime && !!endTime) {
      setTimeRegistrationEnabled(true);
    } else {
      setTimeRegistrationEnabled(false);
    }
  }, [startTime, endTime]);

  const onCreateTimeRegistration = async () => {
    if (startTime && endTime) {
      const body = {
        projectId,
        start: startTime.toDate(),
        end: endTime.toDate(),
      };
      await createTimeRegistrationMutation.mutateAsync(body, {
        onSuccess: () => {
          refetchProject();
        },
        onError: (error) => {
          console.error("Error while creating a time registration", error);
        },
        onSettled: () => {
          setStartTime(null);
          setEndTime(null);
        },
      });
    }
  };

  const rows = useMemo(() => {
    return (
      project?.timeRegistrations.map((timeRegistration) => ({
        ...timeRegistration,
        time: `${
          (new Date(timeRegistration.end).valueOf() -
            new Date(timeRegistration.start).valueOf()) /
          60000
        } minutes`,
      })) ?? []
    );
  }, [project]);

  return (
    <Dialog open={open} maxWidth={"md"} fullWidth onClose={onClose}>
      <ClickAwayListener onClickAway={() => setOpen(false)}>
        <DialogContainer>
          <Typography variant="h4">
            Project #{project?.id} - {project?.name}
          </Typography>

          <AddEntryContainer>
            <Typography variant="h6">Add new time registration</Typography>
            <AddEntryFormContainer>
              <DateTimePicker
                value={startTime}
                disabled={deadlineOver}
                onChange={(value) => setStartTime(value)}
                slotProps={{
                  textField: {
                    size: "small",
                  },
                }}
                label="Start"
              />
              <DateTimePicker
                value={endTime}
                disabled={!startTime}
                onChange={(value) => setEndTime(value)}
                slotProps={{ textField: { size: "small" } }}
                minDateTime={
                  startTime ? moment(startTime).add(30, "m") : undefined
                }
                maxDateTime={moment(project?.deadline)}
                label="End"
              />
              <Button
                variant="contained"
                onClick={onCreateTimeRegistration}
                disabled={!createTimeRegistrationEnabled}
              >
                Add entry
              </Button>
            </AddEntryFormContainer>
            {deadlineOver ? (
              <Typography color={"red"}>
                The deadline for this project is already over. You cannot add
                more time registrations
              </Typography>
            ) : (
              ""
            )}
          </AddEntryContainer>
          <Divider />
          <YourEntriesContainer>
            <Typography variant="h6">Your time registrations</Typography>
            <TableContainer>
              {rows?.length ? (
                <CustomTable rows={rows} columns={columns} size="small" />
              ) : (
                "No time registrations on this project"
              )}
            </TableContainer>
          </YourEntriesContainer>
        </DialogContainer>
      </ClickAwayListener>
    </Dialog>
  );
}
