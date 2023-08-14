<template>
	<div>
		<base-dialog :show="!!error" title="An error occured" :showClose="true" @close="handleError">
			<p>{{ error }}</p>
		</base-dialog>
		<section>
			<form @submit.prevent="submitForm()" v-if="hasEmployees">
				<base-card style="max-width: 50%;">
					<div>
						<div class="form-control" :class="{ invalid: !wtDate.isValid }">
							<label for="wtDate">Date:</label>
							<input type="text" name="wtDate" @blur="clearValidity('wtDate')" v-model="wtDate.val" />
						</div>
						<p class="invalid" v-if="!wtDate.isValid">
							Date must be in a DD.MM.YYYY format.
						</p>
						<hr>
						<table>
							<thead>
								<tr>
									<th>Full name</th>
									<th>Employee worked</th>
									<th>Start time</th>
									<th>End time</th>
								</tr>
							</thead>
							<tbody>
								<tr v-for="employee in loadedEmployees" :key="employee.id">
									<td style="text-align: left;">{{ employee.id + ' - ' + employee.firstName + ' ' + employee.lastName }}
									</td>
									<td>
										<input type="checkbox" v-model="employee.didWork" />
									</td>
									<td>
										<input type="time" name="WorkingTimeDate" v-model="employee.startTime" />
									</td>
									<td>
										<input type="time" name="WorkingTimeDate" v-model="employee.endTime" />
									</td>
								</tr>
							</tbody>
						</table>
						<br>
						<br>
						<div style="text-align: center;">
							<base-button>Save working times</base-button>
						</div>
					</div>
				</base-card>
			</form>
			<p v-else>
				<base-card>
					There are no employees here. There's probably some server side problem. We suggest you try again in a minute or
					so, or just go to the root of the application and start over.
				</base-card>
			</p>
		</section>
	</div>
</template>

<script>
import moment from 'moment';
export default {
	data() {
		return {
			isValid: true,
			error: null,
			wtDate: {
				isValid: true,
				val: ''
			},
			employees: [],
			company: {}
		}
	},
	async created() {
		const allIDs = window.location.pathname.split('/').slice(2);
		const actionLink = allIDs.length === 2 ?
			`${allIDs[0]}/${allIDs[1]}?PageNumber=0&PageSize=10000` :
			`${allIDs[0]}?PageNumber=0&PageSize=10000`;
		try {
			await this.$store.dispatch("employees/loadEmployees", { link: actionLink });
			this.employees = this.$store.getters["employees/employees"];
		} catch (error) {
			this.error = error;
			return;
		}

		try {
			await this.$store.dispatch("companies/loadCompany", { id: allIDs[0] });
		} catch (error) {
			this.error = error;
			return;
		}

		this.wtDate.val = moment(new Date().toLocaleDateString().substring(0, 10)).format("DD.MM.YYYY");

		this.company = this.$store.getters["companies/company"];
		this.employees = this.employees.map(e => {
			return {
				id: e.ID,
				date: this.wtDate.val,
				firstName: e.FirstName,
				lastName: e.LastName,
				endTime: this.company.EndTime,
				startTime: this.company.StartTime,
				didWork: true
			};
		});
	},
	computed: {
		loadedEmployees() {
			return this.employees;
		},
		hasEmployees() {
			return this.$store.getters["employees/hasEmployees"];
		},
		workingDate() {
			return this.wtDate;
		}
	},
	methods: {
		handleError() {
			this.error = null;
		},
		clearValidity(input) {
			this[input].isValid = true;
		},
		validate() {
			this.formIsValid = true;
			if (
				this.wtDate.val === "" ||
				!moment(this.wtDate.val, "DD.MM.YYYY", true).isValid()
			) {
				this.wtDate.isValid = false;
				this.formIsValid = false;
			}
		},
		async submitForm() {
			this.validate();
			if (this.formIsValid) {
				try {
					this.employees.forEach(e => e.date = this.wtDate.val);
					await this.$store.dispatch("workingTimes/addWorkingTimes", { employees: this.employees });
					this.$router.push(`/workingtimes/${this.company.ID}`);
				} catch (error) {
					this.error = error;
				}
			}
		}
	}
}
</script>

<style scoped>
.form-control {
	margin: 0.5rem 0;
}

table {
	text-align: center;
	min-width: 100%;
}

thead {
	margin: 35px 0;
}

hr {
	border: 8px solid black;
	border-radius: 5px;
	margin: 30px 0;
}

label {
	font-weight: bold;
	display: block;
	margin-bottom: 0.5rem;
}

input,
select {
	display: block;
	width: 100%;
	border: 1px solid #ccc;
	font: inherit;
	font-size: 1em;
	border-radius: 15px;
	height: 40px;
}

input:focus {
	background-color: #f0e6fd;
	outline: none;
	border-color: #3d008d;
}

h3 {
	margin: 0.5rem 0;
	font-size: 1rem;
}

.invalid label {
	color: red;
}

.invalid input,
.invalid select {
	border: 1px solid red;
}

p {
	font-weight: bold;
	font-size: large;
}
</style>