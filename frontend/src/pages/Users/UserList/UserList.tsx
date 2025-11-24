import { useEffect, useState } from 'react';
import './UserList.css';
import { DeleteUesrProfile, GetAllUserProfile } from '../../../Service/User-Profile/user-profile';
import type { AllUserDetails, UserProfileQueryParams } from '../../../Models/User-Profile';
import { useNavigate } from 'react-router-dom';
import { notify } from '../../../Service/Toast-Message/Toast-Message';
import { ToastMessageTypes } from '../../../Enums/Toast-Message';
import { FaFilter } from 'react-icons/fa';

interface UserListProps {
  onClickEdit: () => void;
}

function UserList({ onClickEdit }: UserListProps) {
  const [userList, setUserList] = useState<AllUserDetails[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [userType, setUserType] = useState('All');
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const navigate = useNavigate();
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const isFrontDesk = user?.roleName === 'Front Desk';
  const isPatient = user?.roleName === 'Patient';

  useEffect(() => {
    const delayDebounceFn = setTimeout(() => {
      if (searchTerm.length >= 3 || searchTerm.length === 0) {
        loadUsers();
      }
    }, 500);

    return () => clearTimeout(delayDebounceFn);
  }, [searchTerm]);

  useEffect(() => {
    loadUsers();
  }, [userType, currentPage, itemsPerPage]);

  const loadUsers = async () => {
    try {
      const payload: UserProfileQueryParams = {
        SearchTerm: searchTerm,
        UserType: userType,
        PageNumber: currentPage,
        PageSize: itemsPerPage,
        SortByOrder: '',
        SortByColumn: '',
      }
      const response = await GetAllUserProfile(payload);
      console.log(response);
      setUserList(response);
    } catch (error) {
      console.error('Error fetching user list:', error);
      notify('Error fetching user data', ToastMessageTypes.ERROR);
    }
  };

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(event.target.value);
    setCurrentPage(1);
  };

  const handleFilterChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setUserType(event.target.value);
    setCurrentPage(1);
  };

  const handleItemsPerPageChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    console.log(event.target.value, 'items per page');
    setItemsPerPage(Number(event.target.value));
    setCurrentPage(1);
  };

  const navigateToUserProfile = (userId: number) => {
    onClickEdit();
    navigate(`/users?page=userProfile&action=edit&userId=${userId}`);
  };

  async function deleteUser(userId: number): Promise<void> {
    try {
      const response = await DeleteUesrProfile(userId);
      if (response.includes('Success')) {
        notify(response, ToastMessageTypes.SUCCESS);
        await loadUsers();
      } else {
        notify('Something went wrong!!!', ToastMessageTypes.ERROR);
      }
    } catch (err) {
      console.error('Failed to delete user', err);
      notify('Something went wrong!!!', ToastMessageTypes.ERROR);
    }
  }

  // const indexOfLastItem = currentPage * itemsPerPage;
  // const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  // const currentUsers = userList.slice(indexOfFirstItem, indexOfLastItem);
  // console.log(currentUsers, 'currentUsers');
  const totalPages = Math.ceil(userList[0]?.total / itemsPerPage);

  return (
    <div className="user-list-container">
      <div className="toolbar">
        <div className="search-container">
          <input
            type="text"
            placeholder="Search by name..."
            value={searchTerm}
            onChange={handleSearchChange}
            className="search-input"
          />
        </div>
        <div className="filter-container">
          <FaFilter className="filter-icon" />
          <select value={userType} onChange={handleFilterChange} className="filter-select">
            <option value="All">All</option>
            <option value="Patient">Patient</option>
            <option value="Doctor">Doctor</option>
          </select>
        </div>
      </div>
      <div className="table-responsive">
        <table className="app-table">
          <thead>
            <tr>
              <th>User Id</th>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Email</th>
              <th>User Type</th>
              <th>Phone Number</th>
              {!isFrontDesk && !isPatient && <th>Actions</th>}
            </tr>
          </thead>
          <tbody>
            {userList.map((user) => (
              <tr key={user.userId}>
                <td>{user.userId}</td>
                <td>{user.firstName}</td>
                <td>{user.lastName}</td>
                <td>{user.email}</td>
                <td>
                  <span className={`user-type-badge ${user.userType.toLowerCase()}`}>{user.userType}</span>
                </td>
                <td>{user.phoneNumber}</td>
                {!isFrontDesk && !isPatient && (
                  <td>
                    <button className="edit-btn" onClick={() => navigateToUserProfile(user.userId)}>
                      <i className="fas fa-edit"></i>
                    </button>
                    <button className="delete-btn" onClick={() => deleteUser(user.userId)}>
                      <i className="fas fa-trash"></i>
                    </button>
                  </td>
                )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="pagination-controls">
        <div className="items-per-page-selector">
          <label htmlFor="itemsPerPage">Show:</label>
          <select id="itemsPerPage" value={itemsPerPage} onChange={handleItemsPerPageChange}>
            <option value={10}>10</option>
            <option value={20}>20</option>
            <option value={30}>30</option>
          </select>
        </div>
        <div className="pagination">
          <button onClick={() => setCurrentPage(currentPage - 1)} disabled={currentPage === 1}>
            Previous
          </button>
          <span>Page {currentPage} of {totalPages}</span>
          <button onClick={() => setCurrentPage(currentPage + 1)} disabled={currentPage === totalPages}>
            Next
          </button>
        </div>
      </div>
    </div>
  );
}

export default UserList;