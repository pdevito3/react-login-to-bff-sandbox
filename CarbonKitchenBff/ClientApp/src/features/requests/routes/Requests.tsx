import React from 'react';
import { FaEllipsisH, FaExclamation } from 'react-icons/fa';

function Requests() {
	const people = [
		{
			name: '35 y/o Female',
			caseno: 'OC21-987654321',
			submittedTo: 'Genesis Care',
			dueText: 'due today',
			role: 'Admin',
			email: 'jane.cooper@example.com',
			image:
				'https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=4&w=256&h=256&q=60',
		},
	]
	return (		
		<div className="p-12">			
			<div className="py-3 border-b">
				<h1 className="text-3xl font-extrabold tracking-tight text-gray-900">My Requests</h1>
			</div>
			
      <div className="flex flex-col pt-6">
				<div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
					<div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
						<div className="overflow-hidden border-b border-gray-200 shadow sm:rounded-lg">
							<table className="min-w-full divide-y divide-gray-200">
								<thead className="bg-gray-50">
									<tr>
										<th scope="col" className="relative w-24 py-3">
											<span className="sr-only">Actions</span>
										</th>
										<th scope="col" className="relative px-1 py-3">
											<span className="sr-only">Priority</span>
										</th>
										<th
											scope="col"
											className="px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase"
										>
											Case
										</th>
										<th
											scope="col"
											className="px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase"
										>
											Submitted To
										</th>
										<th
											scope="col"
											className="px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase"
										>
											Status
										</th>
									</tr>
								</thead>
								<tbody className="bg-white divide-y divide-gray-200">
									{people.map((person) => (
										<tr key={person.email}>
											<td className="px-10 py-4 text-sm text-gray-500 whitespace-nowrap">
												<FaEllipsisH className="w-5 h-5 text-gray-800" />
											</td>
											<td className="px-1 py-4 text-sm text-gray-500 whitespace-nowrap">
												<FaExclamation className="w-4 h-4 text-red-400" />
											</td>
											<td className="px-6 py-4 whitespace-nowrap">
												<div className="flex items-center">
													<div className="">
														<div className="text-sm font-medium text-gray-900">{person.name}</div>
														<div className="text-sm text-gray-500">{person.caseno}</div>
													</div>
												</div>
											</td>
											<td className="px-6 py-4 whitespace-nowrap">
												<div className="text-sm text-gray-900">{person.submittedTo}</div>
												<div className="text-sm text-red-400">{person.dueText}</div>
											</td>
											<td className="px-6 py-4 whitespace-nowrap">
												<span className="inline-flex px-2 text-xs font-semibold leading-5 text-green-800 bg-green-100 rounded-full">
													Report Ready
												</span>
											</td>
										</tr>
									))}
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
    </div>
	)
}

export { Requests };

