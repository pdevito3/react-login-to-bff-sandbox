import { Menu, Transition } from '@headlessui/react';
import clsx from 'clsx';
import React, { Fragment } from 'react';
import Avatar from 'react-avatar';
import { useAuthUser } from '@/features/Auth';

function PrivateHeader() {
	const { username, logoutUrl } = useAuthUser();

	return (
		<nav className="relative w-full bg-white shadow-md">
			<div className="px-2 mx-auto sm:px-6 lg:px-8">
				<div className="relative flex items-center justify-between h-16">
					<div className="absolute inset-y-0 left-0 flex items-center sm:hidden"></div>
					<div className="flex-1"></div>
					<div className="absolute inset-y-0 right-0 flex items-center pr-2 space-x-5 sm:static sm:inset-auto sm:ml-6 sm:space-x-2 sm:pr-0">
						{/* <button
							type="button"
							className="p-1 text-gray-400 bg-white rounded-full hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
						>
							<span className="sr-only">View notifications</span>
							<svg
								className="w-6 h-6"
								xmlns="http://www.w3.org/2000/svg"
								fill="none"
								viewBox="0 0 24 24"
								stroke="currentColor"
								aria-hidden="true"
							>
								<path
									strokeLinecap="round"
									strokeLinejoin="round"
									strokeWidth="2"
									d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
								></path>
							</svg>
						</button> */}

						{/* <!-- Profile dropdown --> */}
						<Menu as="div" className="relative ml-3">
							<div>
								<Menu.Button className="flex text-sm bg-gray-800 rounded-full focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
									<span className="sr-only">Open user menu</span>
									{/* <img
										className="w-8 h-8 rounded-full"
										src="https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
										alt=""
									/> */}

									<Avatar name={username} round size="36" />
								</Menu.Button>
							</div>
							<Transition
								as={Fragment}
								enter="transition ease-out duration-100"
								enterFrom="transform opacity-0 scale-95"
								enterTo="transform opacity-100 scale-100"
								leave="transition ease-in duration-75"
								leaveFrom="transform opacity-100 scale-100"
								leaveTo="transform opacity-0 scale-95"
							>
								<Menu.Items className="absolute right-0 w-48 py-1 mt-2 origin-top-right bg-white rounded-md shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
									<Menu.Item>
										{({ active }) => (
											<a
												href={logoutUrl?.value}
												className={clsx(
													active ? 'bg-gray-100' : '',
													'block px-4 py-2 text-sm text-gray-700'
												)}
											>
												Logout
											</a>
										)}
									</Menu.Item>
								</Menu.Items>
							</Transition>
						</Menu>
					</div>
				</div>
			</div>
		</nav>
	);
}

export { PrivateHeader };
